using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MicroNutritionist.Common.Database.Models;
using MicroNutritionist.ViewModels.Nutrition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Nutrition.Crud
{
    [QueryProperty(nameof(InjectedNutrition), nameof(AddOrEditNutritionPageViewModel))]
    public partial class AddOrEditNutritionPageViewModel : ObservableObject
    {
        private MicroNutritionist.Common.Database.Models.Nutrition _injectedNutrition;
        public MicroNutritionist.Common.Database.Models.Nutrition InjectedNutrition
        {
            get => _injectedNutrition;
            set
            {
                _injectedNutrition = value;

                MainNutrition = new NutritionViewModel(value);
            }
        }

        [ObservableProperty]
        private NutritionViewModel _mainNutrition;


        [RelayCommand]
        private async Task SaveChanges()
        {
            if(MainNutrition.InnerNutrition.Id == 0)
            {
                await MauiProgram.Database.InsertNutrition(MainNutrition.InnerNutrition);
            }
            else
            {
                await MauiProgram.Database.UpdateNutrition(MainNutrition.InnerNutrition);
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
