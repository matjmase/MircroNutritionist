using MicroNutritionist.ViewModels.Settings;

namespace MicroNutritionist.Views.Settings;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;	
	}
}