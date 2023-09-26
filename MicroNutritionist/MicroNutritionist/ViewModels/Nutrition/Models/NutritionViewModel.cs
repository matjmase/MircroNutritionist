using CommunityToolkit.Mvvm.ComponentModel;

namespace MicroNutritionist.ViewModels.Nutrition.Models
{
    public partial class NutritionViewModel : ObservableObject
    {
        public MicroNutritionist.Common.Database.Models.Nutrition InnerNutrition { get; private set; }

        public string Name
        {
            get => InnerNutrition.Name;
            set
            {
                InnerNutrition.Name = value;
                OnPropertyChanged();
            }
        }

        public NutritionViewModel(MicroNutritionist.Common.Database.Models.Nutrition nutrition)
        {
            InnerNutrition = nutrition;
        }
    }
}
