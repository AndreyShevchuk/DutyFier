using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using Unity;

namespace DutyFier.Client.Wpf.Settings
{
    class AddPersonViewModel
    {
        public Person _person;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddPersonModel AddPersonModel{ get; set; } 
        public AddPersonViewModel()
        {
            _person = new Person();
            AddCommand = new RelayCommands(OnAdd, CanAdd);
            AddPersonModel = new AddPersonModel(MainWindowViewModel.Container.Resolve<IRepository<Person>>());
        }

        public RelayCommands AddCommand { get; set; }
        public void OnAdd()
        {
            _person = new Person();
            _person.Factor = 1;
            _person.FirstName = FirstName;
            _person.LastName = LastName;
            AddPersonModel.AddPersonToDB(_person);
        }
        public bool CanAdd()
        {
            return ((FirstName != null && LastName != null));
        }

    }
}
