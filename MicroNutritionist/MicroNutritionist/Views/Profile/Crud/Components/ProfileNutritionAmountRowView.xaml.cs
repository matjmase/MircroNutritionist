using MicroNutritionist.ViewModels.Nutrition.Models;
using System.Windows.Input;

namespace MicroNutritionist.Views.Profile.Crud.Components;

public partial class ProfileNutritionAmountRowView : ContentView
{

    public static readonly BindableProperty NutritionVMProperty =
  BindableProperty.Create(nameof(NutritionVM), typeof(ProfileNutritionAmountViewModel), typeof(ProfileNutritionAmountRowView));

    public ProfileNutritionAmountViewModel NutritionVM
    {
        get => GetValue(NutritionVMProperty) as ProfileNutritionAmountViewModel;
        set => SetValue(NutritionVMProperty, value);
    }

    public static readonly BindableProperty RemoveItemProperty =
  BindableProperty.Create(nameof(RemoveItem), typeof(ICommand), typeof(ProfileNutritionAmountRowView));

    public ICommand RemoveItem
    {
        get => GetValue(RemoveItemProperty) as ICommand;
        set => SetValue(RemoveItemProperty, value);
    }

    public static readonly BindableProperty EditItemProperty =
  BindableProperty.Create(nameof(EditItem), typeof(ICommand), typeof(ProfileNutritionAmountRowView));

    public ICommand EditItem
    {
        get => GetValue(EditItemProperty) as ICommand;
        set => SetValue(EditItemProperty, value);
    }

    public ProfileNutritionAmountRowView()
	{
		InitializeComponent();
	}
}