using MicroNutritionist.ViewModels.Nutrition.Search;

namespace MicroNutritionist.Views.Nutrition.Search;

public partial class NutritionSearchPage : ContentPage
{
	public NutritionSearchPage(NutritionSearchPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}