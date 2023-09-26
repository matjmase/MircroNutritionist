using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Event.ViewModels
{
    public partial class ConsumptionEventViewModel : ObservableObject
    {
        public MicroNutritionist.Common.Database.Models.ConsumptionEvent Inner { get; private set; }

        public TimeSpan Time
        {
            get => new TimeSpan(Inner.Time.Hour, Inner.Time.Minute, Inner.Time.Second);
            set
            {
                Inner.Time = new DateTime(Inner.Time.Year, Inner.Time.Month, Inner.Time.Day, value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => Inner.Time;
            set
            {
                Inner.Time = new DateTime(value.Year, value.Month, value.Day, Inner.Time.Hour, Inner.Time.Minute, Inner.Time.Second);
                OnPropertyChanged();
            }
        }

        public double Proportion
        {
            get => Inner.Proportion;
            set
            {
                Inner.Proportion = value;
                OnPropertyChanged();
            }
        }

        public ConsumptionEventViewModel(MicroNutritionist.Common.Database.Models.ConsumptionEvent cEvent)
        {
            Inner = cEvent;
        }
    }
}
