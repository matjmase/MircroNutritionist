using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.PubSubMessages.Search;
using MicroNutritionist.ViewModels.Event.ViewModels;
using MicroNutritionist.ViewModels.Search;
using MicroNutritionist.ViewModels.Search.Model;
using MicroNutritionist.Views.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Event.Crud
{
    [QueryProperty(nameof(InjectedProduct), nameof(AddOrEditEventPageViewModel))]
    public partial class AddOrEditEventPageViewModel : ObservableObject
    {
        private MicroNutritionist.Common.Database.Models.ConsumptionEvent _injectedProduct;

        public MicroNutritionist.Common.Database.Models.ConsumptionEvent InjectedProduct
        {
            get { return _injectedProduct; }
            set
            {
                if (_injectedProduct != null)
                {
                    return;
                }

                _injectedProduct = value;
                ConsumptionEvent = new ConsumptionEventViewModel(value);
                Product = MauiProgram.Database.GetProduct(value.ProductId).WaitForResult();
            }
        }

        [ObservableProperty]
        private ConsumptionEventViewModel _consumptionEvent;

        private MicroNutritionist.Common.Database.Models.Product _product;
        public MicroNutritionist.Common.Database.Models.Product Product
        { 
            get => _product;
            set 
            { 
                SetProperty(ref _product, value);
                OnPropertyChanged(nameof(FormIsValid)); 
            }
        }

        public bool FormIsValid => Product != null;


        [RelayCommand]
        private async Task Loaded()
        {
            WeakReferenceMessenger.Default.Register<AddOrEditEventPageViewModel, SelectObjectMessage>(this, async (receiver, editProductMessage) =>
            {
                Product = (MicroNutritionist.Common.Database.Models.Product)editProductMessage.Payload;
            });
        }

        [RelayCommand]
        private async Task UnLoaded()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }


        [RelayCommand]
        private async Task SelectProduct()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllProducts().WaitForResult().ToArray(), e => ((MicroNutritionist.Common.Database.Models.Product)e).Name, StringSearchPageModelType.Select) } });
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            var cEvent = ConsumptionEvent.Inner;

            cEvent.ProductId = Product.Id;  

            if (cEvent.Id == 0)
            {
                await MauiProgram.Database.InsertConsumptionEvent(cEvent);
            }
            else
            { 
                await MauiProgram.Database.UpdateConsumptionEvent(cEvent);
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
