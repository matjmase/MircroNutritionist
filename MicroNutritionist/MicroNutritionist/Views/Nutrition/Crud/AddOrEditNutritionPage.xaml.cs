using MicroNutritionist.ViewModels.Nutrition.Crud;

namespace MicroNutritionist.Views.Nutrition.Crud;

public partial class AddOrEditNutritionPage : ContentPage
{
	public AddOrEditNutritionPage(AddOrEditNutritionPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}