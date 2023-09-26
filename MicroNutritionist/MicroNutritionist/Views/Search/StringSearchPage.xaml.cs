using MicroNutritionist.ViewModels.Search;

namespace MicroNutritionist.Views.Search;

public partial class StringSearchPage : ContentPage
{
    public StringSearchPage(StringSearchPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}