using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Settings.Infrastructure
{
    class PositionToBooleanConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ICollection<Position> positions = value as ICollection<Position>;
            if (positions != null)
            {
                Position position = parameter as Position;
                if (position != null)
                    return positions.Contains(position);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

