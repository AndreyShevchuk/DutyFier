using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Settings
{
    class AddTypeViewModel
    {
        DutyType dutyType;
        public AddTypeViewModel()
        {
            dutyType = new DutyType();
            AddCommand = new RelayCommands(OnAdd, CanAdd);
        }
        public string Name { get; set; }
        public RelayCommands AddCommand { get; set; }

        public void OnAdd()
        {
            //TODO adding to DB logic
        }
        public bool CanAdd()
        {
            return Name != null;
        }
    }
}
