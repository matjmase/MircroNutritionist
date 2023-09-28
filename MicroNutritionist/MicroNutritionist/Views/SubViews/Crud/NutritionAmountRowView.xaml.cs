using System.Windows.Input;

namespace MicroNutritionist.Views.SubViews.Crud;

public partial class NutritionAmountRowView : ContentView
{
    public static readonly BindableProperty MainObjectProperty =
  BindableProperty.Create(nameof(MainObject), typeof(object), typeof(NutritionAmountRowView));

    public object MainObject
    {
        get => GetValue(MainObjectProperty) as object;
        set => SetValue(MainObjectProperty, value);
    }


    public static readonly BindableProperty MainNameProperty =
  BindableProperty.Create(nameof(MainName), typeof(string), typeof(NutritionAmountRowView));

    public string MainName
    {
        get => GetValue(MainNameProperty) as string;
        set => SetValue(MainNameProperty, value);
    }


    public static readonly BindableProperty MainDoubleProperty =
  BindableProperty.Create(nameof(MainDouble), typeof(double?), typeof(NutritionAmountRowView));

    public double? MainDouble
    {
        get => GetValue(MainDoubleProperty) as double?;
        set => SetValue(MainDoubleProperty, value);
    }

    public static readonly BindableProperty RemoveItemProperty =
  BindableProperty.Create(nameof(RemoveItem), typeof(ICommand), typeof(NutritionAmountRowView));

    public ICommand RemoveItem
    {
        get => GetValue(RemoveItemProperty) as ICommand;
        set => SetValue(RemoveItemProperty, value);
    }

    public static readonly BindableProperty EditItemProperty =
  BindableProperty.Create(nameof(EditItem), typeof(ICommand), typeof(NutritionAmountRowView));

    public ICommand EditItem
    {
        get => GetValue(EditItemProperty) as ICommand;
        set => SetValue(EditItemProperty, value);
    }
    public NutritionAmountRowView()
	{
		InitializeComponent();
	}
}