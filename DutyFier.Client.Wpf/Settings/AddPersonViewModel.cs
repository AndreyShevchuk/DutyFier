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
using DutyFier.Core;
using System.Data.Entity;

namespace DutyFier.Client.Wpf.Settings
{
    class AddPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public Person _person;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddPersonModel AddPersonModel{ get; set; } 
        public AddPersonViewModel()
        {
            _person = new Person();
            AddCommand = new RelayCommand(OnAdd);
            AddPersonModel = new AddPersonModel(new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
        }
        public RelayCommand AddCommand { get; set; }
        public void OnAdd(object obj)
        {
            if (!FirstName.Equals("")&&!LastName.Equals(""))
            {
                _person = new Person();
                _person.Factor = 1;
                _person.FirstName = FirstName;
                _person.LastName = LastName;
                FirstName = "";
                OnPropertyChanged(nameof(FirstName));
                LastName = "";
                OnPropertyChanged(nameof(LastName));
                AddPersonModel.AddPersonToDB(_person);
            }
        }
        public bool CanAdd()
        {
            return (FirstName!=null && LastName!=null)&& (!FirstName.Equals("")&&!LastName.Equals(""));
        }

    }
}
