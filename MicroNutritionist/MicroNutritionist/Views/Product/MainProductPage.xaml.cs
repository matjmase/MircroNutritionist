using MicroNutritionist.ViewModels.Product;

namespace MicroNutritionist.Views.Product;

public partial class MainProductPage : ContentPage
{
	public MainProductPage(MainProductPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}