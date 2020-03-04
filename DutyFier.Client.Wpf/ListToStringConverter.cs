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
            string result = "";
            for (int i = 0; i < ((List<DateTime>)value).Count; i++)
            {
                if(i == 10)
                {
                    result += "\n";
                }
                result += "["+((List<DateTime>)value)[i].ToString("dd/MM") +"] ";
            }

            return result;
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
