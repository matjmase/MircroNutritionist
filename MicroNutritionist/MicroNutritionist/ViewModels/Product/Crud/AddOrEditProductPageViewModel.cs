using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.PubSubMessages;
using MicroNutritionist.ViewModels.Nutrition.Crud;
using MicroNutritionist.ViewModels.Nutrition.Models;
using MicroNutritionist.ViewModels.Nutrition.Search;
using MicroNutritionist.ViewModels.Product.Models;
using MicroNutritionist.Views.Nutrition.Crud;
using MicroNutritionist.Views.Nutrition.Search;
using System.Collections.ObjectModel;

namespace MicroNutritionist.ViewModels.Product.Crud
{
    [QueryProperty(nameof(InjectedProduct), nameof(AddOrEditProductPageViewModel))]
    public partial class AddOrEditProductPageViewModel : ObservableObject
    {
        private MicroNutritionist.Common.Database.Models.Product _injectedProduct;

        public MicroNutritionist.Common.Database.Models.Product InjectedProduct
        {
            get { return _injectedProduct; }
            set
            {
                _injectedProduct = value;
                MainProduct = new ProductViewModel(value);

                OriginalNutrients.Clear();
                var blackList = _originalToRemove.Select(e => e.InnerAmount.Id).ToHashSet();
                var nutrientAmounts = MauiProgram.Database.GetAllProductNutritionAmountByProduct(MainProduct.Inner.Id).WaitForResult().Where(e => !blackList.Contains(e.Id));
                foreach (var nutrientAmount in nutrientAmounts)
                {
                    var nutrient = MauiProgram.Database.GetNutrition(nutrientAmount.NutritionId).WaitForResult();
                    OriginalNutrients.Add(new ProductNutritionAmountViewModel(nutrient, nutrientAmount));
                }

                //reload new
                var newNutriCol = NewNutrients.ToList();
                NewNutrients.Clear();

                foreach (var newNutri in newNutriCol)
                {
                    var nutrient = MauiProgram.Database.GetNutrition(newNutri.InnerNutrition.Id).WaitForResult();
                    NewNutrients.Add(new ProductNutritionAmountViewModel(nutrient, newNutri.InnerAmount));
                }
            }
        }

        [ObservableProperty]
        private ProductViewModel mainProduct;
        
        [ObservableProperty]
        private ObservableCollection<ProductNutritionAmountViewModel> newNutrients = new ObservableCollection<ProductNutritionAmountViewModel>();

        [ObservableProperty]
        private ObservableCollection<ProductNutritionAmountViewModel> originalNutrients = new ObservableCollection<ProductNutritionAmountViewModel>();
        private List<ProductNutritionAmountViewModel> _originalToRemove = new List<ProductNutritionAmountViewModel>();

        public AddOrEditProductPageViewModel()
        {
            WeakReferenceMessenger.Default.Register<AddOrEditProductPageViewModel, NutritionSelectedMessage>(this, (recipient, nutrientMessage) =>
            {
                NewNutrients.Add(new ProductNutritionAmountViewModel(nutrientMessage.Nutrition, new Common.Database.Models.ProductNutritionAmount() { NutritionId = nutrientMessage.Nutrition.Id }));
            });
        }

        [RelayCommand]
        private async Task NutrientDetails(ProductNutritionAmountViewModel nutrition)
        {
            await Shell.Current.GoToAsync(nameof(AddOrEditNutritionPage), true, new Dictionary<string, object> { { nameof(AddOrEditNutritionPageViewModel), nutrition.InnerNutrition } });
        }

        [RelayCommand]
        private async Task NutrientOriginalRemove(ProductNutritionAmountViewModel nutrition)
        {
            OriginalNutrients.Remove(nutrition);
            _originalToRemove.Add(nutrition);
        }

        [RelayCommand]
        private async Task NutrientNewlRemove(ProductNutritionAmountViewModel nutrition)
        {
            NewNutrients.Remove(nutrition);
        }

        [RelayCommand]
        private async Task AddNutrient()
        {
            await Shell.Current.GoToAsync(nameof(NutritionSearchPage), true, new Dictionary<string, object> { { nameof(NutritionSearchPageViewModel), OriginalNutrients.Union(NewNutrients).Select(e => e.InnerNutrition.Id).ToHashSet() } });
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            if (MainProduct.Inner.Id == 0)
                await MauiProgram.Database.InsertProduct(MainProduct.Inner);
            else
                await MauiProgram.Database.UpdateProduct(MainProduct.Inner);

            MainProduct = new ProductViewModel(await MauiProgram.Database.GetProductByName(MainProduct.Name));

            foreach (var item in _originalToRemove)
            {
                await MauiProgram.Database.DeleteProductNutritionAmount(item.InnerAmount);
            }

            foreach (var item in OriginalNutrients)
            {
                await MauiProgram.Database.UpdateProductNutritionAmount(item.InnerAmount);
            }

            foreach (var item in NewNutrients.Select(e => e.InnerAmount))
            {
                item.ProductId = MainProduct.Inner.Id;
                await MauiProgram.Database.InsertProductNutritionAmount(item);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
