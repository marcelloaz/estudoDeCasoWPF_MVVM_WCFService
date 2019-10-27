using HAPPYWPF.UI.Comum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HAPPYWPF.Converters
{
    public class ButtonStartBackGroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var conv = new BrushConverter();
                var brush =
                    conv.ConvertFromString(ColorPicker.ColorFromUInt(System.Convert.ToUInt32(value)).ToString())
                        as SolidColorBrush;

                return brush;
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
