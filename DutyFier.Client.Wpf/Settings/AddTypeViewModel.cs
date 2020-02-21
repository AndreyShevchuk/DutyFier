using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.Settings
{
    class AddTypeViewModel
    {
        public DutyType _dutyType;
        //private AdTypeModel AddTypeModel;
        public AddTypeViewModel()
        {
            _dutyType = new DutyType();
            AddCommand = new RelayCommands(OnAdd, CanAdd);
            //AdTypeModel = new 
        }
        public string Name { get; set; }
        public RelayCommands AddCommand { get; set; }

        public void OnAdd()
        {
            _dutyType = new DutyType();
            _dutyType.Name= Name;
            //TODO adding to DB logic
        }
        public bool CanAdd()
        {
            return Name != null;
        }
    }
}
