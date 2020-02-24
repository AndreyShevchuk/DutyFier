using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DutyFier.Core.Models
{
    public class CalendarUI
    {
        public ObservableCollection<DateTime> DateTimes { get; set; }
        public CalendarUI(ObservableCollection<DateTime> dateTimes)
        {
            DateTimes = dateTimes;
        }

        public void UpdateClaendar(IEnumerable<DateTime> dateTimes)
        {
            DateTimes.Clear();
            
            foreach (var date in dateTimes)
            {
                this.DateTimes.Add(date);
            }
        }


    }
}
