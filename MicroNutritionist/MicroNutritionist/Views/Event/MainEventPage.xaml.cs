using MicroNutritionist.ViewModels.Event;

namespace MicroNutritionist.Views.Event;

public partial class MainEventPage : ContentPage
{
	public MainEventPage(MainEventPageViewModel vm)
    {
		InitializeComponent();
		BindingContext = vm;
	}
}