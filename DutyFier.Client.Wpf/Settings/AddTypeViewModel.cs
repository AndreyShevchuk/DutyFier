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
        }
        public string Name { get { return dutyType.Name; } set { dutyType.Name = value; } }
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
