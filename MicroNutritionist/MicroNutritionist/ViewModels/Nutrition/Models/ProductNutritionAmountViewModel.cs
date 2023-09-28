using CommunityToolkit.Mvvm.ComponentModel;

namespace MicroNutritionist.ViewModels.Nutrition.Models
{
    public partial class ProductNutritionAmountViewModel : ObservableObject
    {
        public MicroNutritionist.Common.Database.Models.Nutrition InnerNutrition { get; private set; }
        public MicroNutritionist.Common.Database.Models.ProductNutritionAmount InnerAmount { get; private set; }

        public string Name
        {
            get => InnerNutrition.Name;
            set
            {
                InnerNutrition.Name = value;
                OnPropertyChanged();
            }
        }

        public double? AmountMg
        {
            get => InnerAmount.AmountMg;
            set
            {
                InnerAmount.AmountMg = value == null ? 0.0 : value.Value;
                OnPropertyChanged();
            }
        }

        public ProductNutritionAmountViewModel(MicroNutritionist.Common.Database.Models.Nutrition nutrition, MicroNutritionist.Common.Database.Models.ProductNutritionAmount amount)
        { 
            InnerNutrition = nutrition;
            InnerAmount = amount;
        }
    }
}
