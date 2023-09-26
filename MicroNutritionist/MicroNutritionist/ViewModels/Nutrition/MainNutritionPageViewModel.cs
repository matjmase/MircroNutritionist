using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.Common.Services;
using MicroNutritionist.PubSubMessages.Search;
using MicroNutritionist.ViewModels.Nutrition.Crud;
using MicroNutritionist.ViewModels.Search;
using MicroNutritionist.ViewModels.Search.Model;
using MicroNutritionist.Views.Nutrition.Crud;
using MicroNutritionist.Views.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Nutrition
{
    public partial class MainNutritionPageViewModel : ObservableObject
    {

        private IAlertService _alertService;
        public MainNutritionPageViewModel(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [RelayCommand]
        private async Task Loaded()
        {
            WeakReferenceMessenger.Default.Register<MainNutritionPageViewModel, EditObjectMessage>(this, async (receiver, editProductMessage) =>
            {
                await Shell.Current.GoToAsync(nameof(AddOrEditNutritionPage), true, new Dictionary<string, object> { { nameof(AddOrEditNutritionPageViewModel), (MicroNutritionist.Common.Database.Models.Nutrition)editProductMessage.Payload } });
            });
            WeakReferenceMessenger.Default.Register<MainNutritionPageViewModel, DeleteObjectMessage>(this, async (receiver, deleteProductMessage) =>
            {
                var answer = await _alertService.ShowConfirmationAsync("Confirmation", "Are you sure you want to delete this nutrition?");

                if (answer)
                {
                    await MauiProgram.Database.DeleteNutritionCascade((MicroNutritionist.Common.Database.Models.Nutrition)deleteProductMessage.Payload);
                }
            });
        }

        [RelayCommand]
        private async Task UnLoaded()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

        [RelayCommand]
        public async Task AddNewNutrition()
        {
            await Shell.Current.GoToAsync(nameof(AddOrEditNutritionPage), true, new Dictionary<string, object> { { nameof(AddOrEditNutritionPageViewModel), new MicroNutritionist.Common.Database.Models.Nutrition() } });
        }

        [RelayCommand]
        public async Task EditNutrition()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllNutrition().WaitForResult().ToArray(), e => ((MicroNutritionist.Common.Database.Models.Nutrition)e).Name, StringSearchPageModelType.Edit) } });
        }

        [RelayCommand]
        public async Task DeleteNutrition()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllNutrition().WaitForResult().ToArray(), e => ((MicroNutritionist.Common.Database.Models.Nutrition)e).Name, StringSearchPageModelType.Delete) } });
        }
    }
}
