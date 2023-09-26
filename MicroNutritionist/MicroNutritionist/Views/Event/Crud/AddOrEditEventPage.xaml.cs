using MicroNutritionist.ViewModels.Event.Crud;

namespace MicroNutritionist.Views.Event.Crud;

public partial class AddOrEditEventPage : ContentPage
{
	public AddOrEditEventPage(AddOrEditEventPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}