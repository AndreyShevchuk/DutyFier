using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DutyFier.Client.Wpf
{

    [ValueConversion(typeof(List<DateTime>), typeof(DateTime))]
    class ListToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a String");
            
            
                return String.Join(", ", ((List<DateTime>)value).Select(date => $"[{date.Day}/{date.Month}]").ToArray());
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
