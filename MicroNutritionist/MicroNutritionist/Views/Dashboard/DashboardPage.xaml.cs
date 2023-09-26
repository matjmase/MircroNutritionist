using MicroNutritionist.ViewModels.Dashboard;
using Microsoft.Maui.Layouts;

namespace MicroNutritionist.Views.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}