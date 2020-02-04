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
        
        public Person _person { get; set; }
        public string FirstName { get { return _person.FirstName; } set { _person.FirstName = value; } }
        public string LastName { get { return _person.LastName; } set { _person.LastName = value; } }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        //логика добавления в БД
                    }));
            }
        }

    }
}
