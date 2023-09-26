using MicroNutritionist.ViewModels.Nutrition;

namespace MicroNutritionist.Views.Nutrition;

public partial class MainNutritionPage : ContentPage
{
	public MainNutritionPage(MainNutritionPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}