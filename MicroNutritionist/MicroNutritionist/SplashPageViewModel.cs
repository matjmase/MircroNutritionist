using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MicroNutritionist.Views;
using MicroNutritionist.Views.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist
{
    public partial class SplashPageViewModel : ObservableObject
    {
        [RelayCommand]
        public async Task ProceedToApplication()
        {
            await Shell.Current.GoToAsync(nameof(DashboardPage));
        }
    }
}
