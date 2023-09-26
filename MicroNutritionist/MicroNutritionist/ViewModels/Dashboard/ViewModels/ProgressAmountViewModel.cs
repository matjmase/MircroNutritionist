using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Dashboard.ViewModels
{
    public partial class ProgressAmountViewModel : ObservableObject
    {
        [ObservableProperty]
        private double _progress;
        [ObservableProperty]
        private string _name;

        public ProgressAmountViewModel(string name, double current, double total)
        { 
            Progress = current / total;
            Name = name;
        }
    }
}
