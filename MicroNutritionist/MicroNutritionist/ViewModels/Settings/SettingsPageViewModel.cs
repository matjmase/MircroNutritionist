using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MicroNutritionist.Common.Services;
using MicroNutritionist.Views.Event;
using MicroNutritionist.Views.Nutrition;
using MicroNutritionist.Views.Product;
using MicroNutritionist.Views.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Settings
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        private IAlertService _alertService;

        public SettingsPageViewModel(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [RelayCommand]
        public async Task ProceedToProfile()
        {
            await Shell.Current.GoToAsync(nameof(MainProfilePage));
        }

        [RelayCommand]
        public async Task ProceedToProduct()
        {
            await Shell.Current.GoToAsync(nameof(MainProductPage));
        }

        [RelayCommand]
        public async Task ProceedToNutrition()
        {
            await Shell.Current.GoToAsync(nameof(MainNutritionPage));
        }
        [RelayCommand]
        public async Task ProceedToEvent()
        {
            await Shell.Current.GoToAsync(nameof(MainEventPage));
        }
        [RelayCommand]
        public async Task ClearDatabase()
        {
            var answer = await _alertService.ShowConfirmationAsync("Confirmation", "Are you sure you want to clear the database?");

            if (answer)
            {
                await MauiProgram.ClearDatabase();
            }
        }
    }
}
