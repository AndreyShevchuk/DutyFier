using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace DutyFier.Core.Models
{
    public class CalendarUI
    {
        public ObservableCollection<DateTime> DateTimes { get; }
        public CalendarUI(ObservableCollection<DateTime> dateTimes)
        {
            DateTimes = dateTimes;
        }

        public void UpdateClaendar(IEnumerable<DateTime> dateTimes)
        {
            if (DateTimes != null)
            {
                DateTimes.Clear();

                foreach (var date in dateTimes)
                {
                    this.DateTimes.Add(date);
                }
            }
        }

        public List<DateTime> GetSelectedDates()
        {
            return DateTimes.ToList();
        }
    }
}
