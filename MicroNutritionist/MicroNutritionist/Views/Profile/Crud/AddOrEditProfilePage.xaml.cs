using MicroNutritionist.ViewModels.Profile.Crud;

namespace MicroNutritionist.Views.Profile.Crud;

public partial class AddOrEditProfilePage : ContentPage
{
	public AddOrEditProfilePage(AddOrEditProfilePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}