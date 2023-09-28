using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Nutrition.Models
{
    public partial class ProfileNutritionAmountViewModel : ObservableObject
    {
        public MicroNutritionist.Common.Database.Models.Nutrition InnerNutrition { get; private set; }
        public MicroNutritionist.Common.Database.Models.ProfileNutritionAmount InnerAmount { get; private set; }

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

        public ProfileNutritionAmountViewModel(MicroNutritionist.Common.Database.Models.Nutrition nutrition, MicroNutritionist.Common.Database.Models.ProfileNutritionAmount amount)
        {
            InnerNutrition = nutrition;
            InnerAmount = amount;
        }
    }
}
