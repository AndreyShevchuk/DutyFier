using DutyFier.Client.Wpf.State;
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
        public GenerateContext context { get; set; }


        public ResultViewModel(GenerateContext context)
        {
            this.context = context;
            Dutys = context.duties;
        }


        public RelayCommand ComandChangeExecutors
        {
            get
            {
                return comandChangeExecutors ??
                (comandChangeExecutors = new RelayCommand(obj =>
                {
                    EditDutyView apv = new EditDutyView();
                    apv.DataContext = new EditDutyViewModel(new Duty(SelectedDuty));
                    if (apv.ShowDialog() == true)
                    {

                    }
                },
                (obj) => true)); // Можна буде написати функцію яка буде перевіряти в загалі є на кого поміняти чи ні;
            }
        }
    }
}
