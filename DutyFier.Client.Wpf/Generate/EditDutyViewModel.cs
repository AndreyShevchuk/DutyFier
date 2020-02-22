using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Generate
{
    class EditDutyViewModel
    {
        public Duty ChangeDuty { get; set; }

        public ObservableCollection<Person> pp { get; set; } 

        public EditDutyViewModel(Duty duty)
        {
            ChangeDuty = duty;
            pp = new ObservableCollection<Person>(duty.Executors.Select(e => e.Person));
        }
        public EditDutyViewModel()
        {

        }

    }
}
