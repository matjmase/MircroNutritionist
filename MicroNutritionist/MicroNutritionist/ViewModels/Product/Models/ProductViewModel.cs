using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Product.Models
{
    public partial class ProductViewModel : ObservableObject
    {
        public MicroNutritionist.Common.Database.Models.Product Inner { get; private set; }

        public string Name
        {
            get => Inner.Name;
            set
            {
                Inner.Name = value;
                OnPropertyChanged();
            }
        }
        public int Calories
        {
            get => Inner.Calories;
            set
            {
                Inner.Calories = value;
                OnPropertyChanged();
            }
        }
        public string ServingDescription
        {
            get => Inner.ServingDescription;
            set
            {
                Inner.ServingDescription = value;
                OnPropertyChanged();
            }
        }

        public ProductViewModel(MicroNutritionist.Common.Database.Models.Product product)
        {
            Inner = product;
        }
    }
}
