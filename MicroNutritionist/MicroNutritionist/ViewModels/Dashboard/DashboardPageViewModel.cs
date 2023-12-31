﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MicroNutritionist.Common.Database.Models;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.ViewModels.Dashboard.ViewModels;
using MicroNutritionist.Views.Event;
using MicroNutritionist.Views.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Dashboard
{
    public partial class DashboardPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DescriptionEnumViewModel> _timeFrames = new ObservableCollection<ViewModels.DescriptionEnumViewModel>();

        [ObservableProperty]
        private ObservableCollection<MicroNutritionist.Common.Database.Models.Profile> _totalProfiles = new ObservableCollection<Common.Database.Models.Profile>();

        [ObservableProperty]
        private ProgressAmountViewModel _calorieAmounts;
        [ObservableProperty]
        private ObservableCollection<ProgressAmountViewModel> _nutritionAmounts = new ObservableCollection<ProgressAmountViewModel>();
        [ObservableProperty]
        private ObservableCollection<ProgressAmountViewModel> _extraNutritionAmounts = new ObservableCollection<ProgressAmountViewModel>();

        private MicroNutritionist.Common.Database.Models.Profile _selectedProfile;
        public MicroNutritionist.Common.Database.Models.Profile SelectedProfile
        {
            get => _selectedProfile;
            set
            {
                SetProperty(ref _selectedProfile, value);

                InitializeNutritionAmounts();
            }
        }

        private DescriptionEnumViewModel _selectedTimeframe;
        public DescriptionEnumViewModel SelectedTimeframe
        {
            get => _selectedTimeframe;
            set
            {
                SetProperty(ref _selectedTimeframe, value);

                InitializeNutritionAmounts();
            }
        }

        public DashboardPageViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            var allProfiles = MauiProgram.Database.GetAllProfiles().WaitForResult();

            TotalProfiles.Clear();
            foreach (var profile in allProfiles)
            {
                TotalProfiles.Add(profile);
            }

            TimeFrames.Clear();
            TimeFrames.Add(new DescriptionEnumViewModel(DashboardTimeframe.Past24Hours, "Past 24 Hours"));
            TimeFrames.Add(new DescriptionEnumViewModel(DashboardTimeframe.SinceMidnight, "Since Midnight"));
        }

        private void InitializeNutritionAmounts()
        {
            CalorieAmounts = null;
            NutritionAmounts.Clear();
            ExtraNutritionAmounts.Clear();
            if (SelectedProfile == null || SelectedTimeframe == null)
                return;

            // Get Data from DB ready
            var profileAmounts = MauiProgram.Database.GetProfileNutritionAmountByProfile(SelectedProfile.Id).WaitForResult();
            List<ConsumptionEvent> consumptionEvents;

            switch (SelectedTimeframe.EnumValue)
            {
                case DashboardTimeframe.Past24Hours:
                    consumptionEvents = MauiProgram.Database.GetAllConsumptionEventByTimeWindow(DateTime.Now.AddHours(-24), DateTime.Now).WaitForResult();
                    break;
                case DashboardTimeframe.SinceMidnight:
                    consumptionEvents = MauiProgram.Database.GetAllConsumptionEventByTimeWindow(DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second), DateTime.Now).WaitForResult();
                    break;
                default:
                    throw new NotImplementedException();
            }

            var productsIds = consumptionEvents.Select(e => e.ProductId).ToHashSet();
            var productAmounts = MauiProgram.Database.GetAllProductNutritionAmount().WaitForResult().Where(e => productsIds.Contains(e.ProductId));
            var products = MauiProgram.Database.GetAllProducts().WaitForResult().ToDictionary(e => e.Id);

            var nutritionDict = MauiProgram.Database.GetAllNutrition().WaitForResult().ToDictionary(e => e.Id);

            var profileAmtDict = profileAmounts.ToDictionary(e => e.NutritionId);
            var consumptionDict = consumptionEvents.ToDictionary(e => e.ProductId);

            // Get Calories of all products scaled by portion
            var calorieCounter = 0.0;
            foreach (var consumeEvent in consumptionEvents)
            {
                calorieCounter += products[consumeEvent.ProductId].Calories * consumeEvent.Proportion;
            }

            CalorieAmounts = FormateProgressAmountNameForProduct("Calories", calorieCounter, SelectedProfile.Calories);

            // Sum them together by Nutrients
            var total = new Dictionary<int, double>();
            foreach (var prodAmount in productAmounts)
            {
                if (!total.ContainsKey(prodAmount.NutritionId))
                {
                    total.Add(prodAmount.NutritionId, 0);
                }

                total[prodAmount.NutritionId] += consumptionDict[prodAmount.ProductId].Proportion * prodAmount.AmountMg;
            }

            // Essential Nutrients
            foreach (var profileAmt in profileAmounts)
            {
                var amount = 0.0;

                if (total.ContainsKey(profileAmt.NutritionId))
                {
                    amount = total[profileAmt.NutritionId];
                }

                NutritionAmounts.Add(FormateProgressAmountNameForNutrition(nutritionDict[profileAmt.NutritionId].Name, amount, profileAmt.AmountMg));
            }

            // Extra Nutrients
            var already = profileAmounts.Select(e => e.NutritionId).ToHashSet();

            foreach (var item in total)
            {
                if (already.Contains(item.Key))
                    continue;

                ExtraNutritionAmounts.Add(FormateProgressAmountNameForNutrition(nutritionDict[item.Key].Name, item.Value, 0));
            }

            // Sorts
            NutritionAmounts = new ObservableCollection<ProgressAmountViewModel>(NutritionAmounts.OrderBy(e => e.Progress).ThenBy(e => e.Name));
            ExtraNutritionAmounts = new ObservableCollection<ProgressAmountViewModel>(ExtraNutritionAmounts.OrderBy(e => e.Name));
        }


        private ProgressAmountViewModel FormateProgressAmountNameForProduct(string name, double current, double total)
        {
            if (total == 0)
            {
                return new ProgressAmountViewModel($"{name} - {String.Format("{0:0.}", current)} / {String.Format("{0:0.}", total)}", current, total);
            }
            else
            {
                return new ProgressAmountViewModel($"{name} - {String.Format("{0:0.}", current)} / {String.Format("{0:0.}", total)} (%{String.Format("{0:0.0}", current / total * 100)})", current, total);
            }
        }
        private ProgressAmountViewModel FormateProgressAmountNameForNutrition(string name, double current, double total)
        {
            if (total == 0)
            {
                return new ProgressAmountViewModel($"{name} - {String.Format("{0:0.}", current)}mg / {String.Format("{0:0.}", total)}mg", current, total);
            }
            else
            {
                return new ProgressAmountViewModel($"{name} - {String.Format("{0:0.}", current)}mg / {String.Format("{0:0.}", total)}mg (%{String.Format("{0:0.0}", current / total * 100)})", current, total);
            }
        }

        [RelayCommand]
        public async Task ProceedToEvent()
        {
            await Shell.Current.GoToAsync(nameof(MainEventPage));
            Initialize();
        }

        [RelayCommand]
        public async Task ProceedToSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
            Initialize();
        }
    }

    public enum DashboardTimeframe
    { 
        Past24Hours,
        SinceMidnight,
    }
}
