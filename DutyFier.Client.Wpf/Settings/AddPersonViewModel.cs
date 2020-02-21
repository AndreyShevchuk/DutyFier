using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using Unity;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
            AddCommand = new RelayCommand(OnAdd);
            AddPersonModel = new AddPersonModel(MainWindowViewModel.Container.Resolve<IRepository<Person>>());
        }
        public RelayCommand AddCommand { get; set; }
        public void OnAdd(object obj)
        {
            if (CanAdd())
            {
                _person = new Person();
                _person.Factor = 1;
                _person.FirstName = FirstName;
                _person.LastName = LastName;
                FirstName = "";
                LastName = "";
                AddPersonModel.AddPersonToDB(_person);
            }
        }
        public bool CanAdd()
        {
            return (FirstName!=null && LastName!=null)&& (!FirstName.Equals("")&&!LastName.Equals(""));
        }

    }
}
