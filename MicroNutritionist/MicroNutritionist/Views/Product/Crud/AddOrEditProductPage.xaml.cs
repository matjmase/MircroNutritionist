using MicroNutritionist.ViewModels.Product.Crud;

namespace MicroNutritionist.Views.Product.Crud;

public partial class AddOrEditProductPage : ContentPage
{
	public AddOrEditProductPage(AddOrEditProductPageViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
	}
}