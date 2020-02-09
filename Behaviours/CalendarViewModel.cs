using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behaviours
{
    class CalendarViewModel
    {
        public CalendarViewModel()
        {
            this.SelectedDates = new ObservableCollection<DateTime>();
        }

        public ObservableCollection<DateTime> SelectedDates
        {
            get;
            set;
        }
    }
}
