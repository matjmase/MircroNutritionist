using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.Common.Converters
{
    public class ProportionToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colors = (Microsoft.Maui.Graphics.Color[])parameter;

            if (colors.Length != 2)
            {
                throw new ArgumentException();
            }

            var doubleVal = (double)value;  

            return doubleVal < 1.0 ? colors[0] : colors[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
