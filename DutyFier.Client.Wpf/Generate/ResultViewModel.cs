using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Generate
{
    class ResultViewModel
    {
        public ObservableCollection<Duty> Dutys { get; set; }
        public Duty SelectedDuty { get; set; }
        
        private RelayCommand comandChangeExecutors;
        public ResultViewModel()
        {
            Dutys = new ObservableCollection<Duty>();
        }


        public RelayCommand ComandChangeExecutors
        {
            get
            {
                return comandChangeExecutors ??
                (comandChangeExecutors = new RelayCommand(obj =>
                {
                    
                },
                (obj) => true)); // Можна буде написати функцію яка буде перевіряти в загалі є на кого поміняти чи ні;
            }
        }





    }
}
