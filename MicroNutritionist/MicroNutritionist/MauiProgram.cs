using CommunityToolkit.Maui;
using MicroNutritionist.Common.Database;
using MicroNutritionist.Common.Services;
using MicroNutritionist.ViewModels;
using MicroNutritionist.ViewModels.Dashboard;
using MicroNutritionist.ViewModels.Event;
using MicroNutritionist.ViewModels.Event.Crud;
using MicroNutritionist.ViewModels.Nutrition;
using MicroNutritionist.ViewModels.Nutrition.Crud;
using MicroNutritionist.ViewModels.Nutrition.Search;
using MicroNutritionist.ViewModels.Product;
using MicroNutritionist.ViewModels.Product.Crud;
using MicroNutritionist.ViewModels.Profile;
using MicroNutritionist.ViewModels.Profile.Crud;
using MicroNutritionist.ViewModels.Search;
using MicroNutritionist.ViewModels.Settings;
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
using Microsoft.Extensions.Logging;

namespace MicroNutritionist;

public static class MauiProgram
{
    private static NutritionDatabase database;

    public static NutritionDatabase Database
    {
        get
        {
            if (database == null)
            {
                database = new NutritionDatabase();
            }
            return database;
        }
    }

    public static async Task ClearDatabase()
    { 
        await Database.ClearDatabase();
        database = new NutritionDatabase();
    }

    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<IAlertService, AlertService>();

        builder.Services.AddTransient<SplashPage>();
        builder.Services.AddTransient<SplashPageViewModel>();

        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<DashboardPageViewModel>();

        builder.Services.AddTransient<MainNutritionPage>();
        builder.Services.AddTransient<MainNutritionPageViewModel>();

        builder.Services.AddTransient<AddOrEditNutritionPage>();
        builder.Services.AddTransient<AddOrEditNutritionPageViewModel>();

        builder.Services.AddTransient<MainProductPage>();
        builder.Services.AddTransient<MainProductPageViewModel>();

        builder.Services.AddTransient<AddOrEditProductPage>();
        builder.Services.AddTransient<AddOrEditProductPageViewModel>();

        builder.Services.AddTransient<MainProfilePage>();
        builder.Services.AddTransient<MainProfilePageViewModel>();

        builder.Services.AddTransient<AddOrEditProfilePage>();
        builder.Services.AddTransient<AddOrEditProfilePageViewModel>();

        builder.Services.AddTransient<MainEventPage>();
        builder.Services.AddTransient<MainEventPageViewModel>();

        builder.Services.AddTransient<AddOrEditEventPage>();
        builder.Services.AddTransient<AddOrEditEventPageViewModel>();

        builder.Services.AddTransient<NutritionSearchPage>();
        builder.Services.AddTransient<NutritionSearchPageViewModel>();

        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsPageViewModel>();

        builder.Services.AddTransient<StringSearchPage>();
        builder.Services.AddTransient<StringSearchPageViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
