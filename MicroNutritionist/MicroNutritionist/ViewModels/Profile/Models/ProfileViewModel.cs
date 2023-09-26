using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Profile.Models
{
    public partial class ProfileViewModel : ObservableObject
    {
        public Common.Database.Models.Profile Inner { get; private set; }


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
        public string Description
        {
            get => Inner.Description;
            set
            {
                Inner.Description = value;
                OnPropertyChanged();
            }
        }

        public ProfileViewModel(MicroNutritionist.Common.Database.Models.Profile profile)
        {
            Inner = profile;
        }

    }
}
