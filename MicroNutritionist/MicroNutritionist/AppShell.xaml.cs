using MicroNutritionist.Views;
using MicroNutritionist.Views.Dashboard;
using MicroNutritionist.Views.Event;
using MicroNutritionist.Views.Event.Crud;
using MicroNutritionist.Views.Nutrition;
using MicroNutritionist.Views.Nutrition.Crud;
using MicroNutritionist.Views.Nutrition.Search;
using MicroNutritionist.Views.Product;
using MicroNutritionist.Views.Product.Crud;
using MicroNutritionist.Views.Profile;
using MicroNutritionist.Views.Profile.Crud;
using MicroNutritionist.Views.Search;
using MicroNutritionist.Views.Settings;

namespace MicroNutritionist;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));

        Routing.RegisterRoute(nameof(MainProductPage), typeof(MainProductPage));
        Routing.RegisterRoute(nameof(AddOrEditProductPage), typeof(AddOrEditProductPage));

        Routing.RegisterRoute(nameof(MainProfilePage), typeof(MainProfilePage));
        Routing.RegisterRoute(nameof(AddOrEditProfilePage), typeof(AddOrEditProfilePage));

        Routing.RegisterRoute(nameof(MainEventPage), typeof(MainEventPage));
        Routing.RegisterRoute(nameof(AddOrEditEventPage), typeof(AddOrEditEventPage));

        Routing.RegisterRoute(nameof(MainNutritionPage), typeof(MainNutritionPage));
        Routing.RegisterRoute(nameof(AddOrEditNutritionPage), typeof(AddOrEditNutritionPage));

        Routing.RegisterRoute(nameof(NutritionSearchPage), typeof(NutritionSearchPage));

        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

        Routing.RegisterRoute(nameof(StringSearchPage), typeof(StringSearchPage));
    }
}
