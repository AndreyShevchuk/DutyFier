using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Settings
{
    class AddPersonViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddPersonViewModel()
        {
            AddCommand = new RelayCommands(OnAdd, CanAdd);
        }

        public RelayCommands AddCommand { get; set; }
        public void OnAdd()
        {
            //TODO adding to DB logic
        }
        public bool CanAdd()
        {
            return ((FirstName != null && LastName != null));
        }

    }
}
