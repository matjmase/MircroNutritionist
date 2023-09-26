using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.Common.Services;
using MicroNutritionist.PubSubMessages.Search;
using MicroNutritionist.ViewModels.Event.Crud;
using MicroNutritionist.ViewModels.Search;
using MicroNutritionist.ViewModels.Search.Model;
using MicroNutritionist.Views.Event.Crud;
using MicroNutritionist.Views.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Event
{
    public partial class MainEventPageViewModel : ObservableObject
    {
        private IAlertService _alertService;

        public MainEventPageViewModel(IAlertService alertService)
        {
            _alertService = alertService;
        }


        [RelayCommand]
        private async Task Loaded()
        {
            WeakReferenceMessenger.Default.Register<MainEventPageViewModel, EditObjectMessage>(this, async (receiver, editProductMessage) =>
            {
                await Shell.Current.GoToAsync(nameof(AddOrEditEventPage), true, new Dictionary<string, object> { { nameof(AddOrEditEventPageViewModel), (MicroNutritionist.Common.Database.Models.ConsumptionEvent)editProductMessage.Payload } });
            });
            WeakReferenceMessenger.Default.Register<MainEventPageViewModel, DeleteObjectMessage>(this, async (receiver, deleteProductMessage) =>
            {
                var answer = await _alertService.ShowConfirmationAsync("Confirmation", "Are you sure you want to delete this product?");

                if (answer)
                {
                    await MauiProgram.Database.DeleteConsumptionEvent((MicroNutritionist.Common.Database.Models.ConsumptionEvent)deleteProductMessage.Payload);
                }
            });
        }

        [RelayCommand]
        private async Task UnLoaded()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

        [RelayCommand]
        private async Task AddNewEvent()
        {
            await Shell.Current.GoToAsync(nameof(AddOrEditEventPage), true, new Dictionary<string, object> { { nameof(AddOrEditEventPageViewModel), new MicroNutritionist.Common.Database.Models.ConsumptionEvent() { Time = DateTime.Now } } });
        }
        [RelayCommand]
        private async Task EditEvent()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllConsumptionEvent().WaitForResult().ToArray(), e => ((MicroNutritionist.Common.Database.Models.ConsumptionEvent)e).Time.ToString(), StringSearchPageModelType.Edit) } });
        }
        [RelayCommand]
        private async Task DeleteEvent()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllConsumptionEvent().WaitForResult().ToArray(), e => ((MicroNutritionist.Common.Database.Models.ConsumptionEvent)e).Time.ToString(), StringSearchPageModelType.Delete) } });
        }
    }
}
