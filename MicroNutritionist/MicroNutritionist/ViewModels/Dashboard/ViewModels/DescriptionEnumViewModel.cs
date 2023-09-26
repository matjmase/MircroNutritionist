using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Dashboard.ViewModels
{
    public partial class DescriptionEnumViewModel : ObservableObject
    {
        [ObservableProperty]
        private DashboardTimeframe _enumValue;
        [ObservableProperty]
        private string _description;

        public DescriptionEnumViewModel(DashboardTimeframe enumValue, string description)
        {
            EnumValue = enumValue;
            Description = description;
        }
    }
}
