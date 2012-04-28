using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PhoneApp1.Converter
{
    public class EvenOddColorConvert : IValueConverter
    {
        SolidColorBrush even = new SolidColorBrush(Color.FromArgb(255, 55, 55, 55)); // Set these two brushes to your alternating background colours.
        SolidColorBrush odd = new SolidColorBrush(Colors.Black);

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var num = (int)value;
            return ((num & 0x01) == 0) ? even : odd;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
