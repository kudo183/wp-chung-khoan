using System;
using System.Windows.Data;
using System.Windows.Media;

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
