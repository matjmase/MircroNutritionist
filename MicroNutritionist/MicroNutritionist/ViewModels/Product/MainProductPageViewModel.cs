using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.Common.Services;
using MicroNutritionist.PubSubMessages.Search;
using MicroNutritionist.ViewModels.Product.Crud;
using MicroNutritionist.ViewModels.Search;
using MicroNutritionist.ViewModels.Search.Model;
using MicroNutritionist.Views.Product.Crud;
using MicroNutritionist.Views.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Product
{
    public partial class MainProductPageViewModel : ObservableObject
    {
        private IAlertService _alertService;
        public MainProductPageViewModel(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [RelayCommand]
        private async Task Loaded()
        {
            WeakReferenceMessenger.Default.Register<MainProductPageViewModel, EditObjectMessage>(this, async (receiver, editProductMessage) =>
            {
                await Shell.Current.GoToAsync(nameof(AddOrEditProductPage), true, new Dictionary<string, object> { { nameof(AddOrEditProductPageViewModel), (MicroNutritionist.Common.Database.Models.Product)editProductMessage.Payload } });
            });
            WeakReferenceMessenger.Default.Register<MainProductPageViewModel, DeleteObjectMessage>(this, async (receiver, deleteProductMessage) =>
            {
                var answer = await _alertService.ShowConfirmationAsync("Confirmation", "Are you sure you want to delete this product?");

                if (answer)
                {
                    await MauiProgram.Database.DeleteProductCascade((MicroNutritionist.Common.Database.Models.Product)deleteProductMessage.Payload);
                }
            });
        }

        [RelayCommand]
        private async Task UnLoaded()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

        [RelayCommand]
        public async Task AddNewProduct()
        {
            await Shell.Current.GoToAsync(nameof(AddOrEditProductPage), true, new Dictionary<string, object> { { nameof(AddOrEditProductPageViewModel), new MicroNutritionist.Common.Database.Models.Product() } });
        }

        [RelayCommand]
        public async Task EditProduct()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllProducts().WaitForResult().ToArray(), e => ((MicroNutritionist.Common.Database.Models.Product)e).Name, StringSearchPageModelType.Edit) } });
        }

        [RelayCommand]
        public async Task DeleteProduct()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllProducts().WaitForResult().ToArray(), e => ((MicroNutritionist.Common.Database.Models.Product)e).Name, StringSearchPageModelType.Delete) } });
        }
    }
}
