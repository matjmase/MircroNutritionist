using MicroNutritionist.ViewModels.Nutrition.Models;
using System.Windows.Input;

namespace MicroNutritionist.Views.Product.Crud.Components;

public partial class ProductNutritionAmountRowView : ContentView
{
    public static readonly BindableProperty NutritionVMProperty =
  BindableProperty.Create(nameof(NutritionVM), typeof(ProductNutritionAmountViewModel), typeof(ProductNutritionAmountRowView));

    public ProductNutritionAmountViewModel NutritionVM
    {
        get => GetValue(NutritionVMProperty) as ProductNutritionAmountViewModel;
        set => SetValue(NutritionVMProperty, value);
    }

    public static readonly BindableProperty RemoveItemProperty =
  BindableProperty.Create(nameof(RemoveItem), typeof(ICommand), typeof(ProductNutritionAmountRowView));

    public ICommand RemoveItem
    {
        get => GetValue(RemoveItemProperty) as ICommand;
        set => SetValue(RemoveItemProperty, value);
    }

    public static readonly BindableProperty EditItemProperty =
  BindableProperty.Create(nameof(EditItem), typeof(ICommand), typeof(ProductNutritionAmountRowView));

    public ICommand EditItem
    {
        get => GetValue(EditItemProperty) as ICommand;
        set => SetValue(EditItemProperty, value);
    }


    public ProductNutritionAmountRowView()
    {
        InitializeComponent();
    }
}