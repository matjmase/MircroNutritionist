using MicroNutritionist.ViewModels.Profile;

namespace MicroNutritionist.Views.Profile;

public partial class MainProfilePage : ContentPage
{
	public MainProfilePage(MainProfilePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}