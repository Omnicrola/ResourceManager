using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Converters;

namespace ResourceManagment.Data.Converters
{
    [ValueConversion(typeof(Color), typeof(Brush))]
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color)
            {
                var color = (Color)value;
                return new SolidColorBrush(color);
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorBrush = value as SolidColorBrush;
            var brush = colorBrush;
            return brush?.Color;
        }
    }
}
