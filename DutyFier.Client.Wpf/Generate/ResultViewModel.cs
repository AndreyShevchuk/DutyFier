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
        public GenerateContext context { get; set; }
        public RelayCommands ComandChangeExecutors { get; set; }


        public ResultViewModel(GenerateContext context)
        {
            this.context = context;
            Dutys = context.duties;
            ComandChangeExecutors = new RelayCommands(ShowDialogWindowChangeDuty, () => true);
        }

        private void ShowDialogWindowChangeDuty()
        {
            EditDutyView apv = new EditDutyView();
            apv.DataContext = new EditDutyViewModel(SelectedDuty, context.personRepository.GetAll());
            if (apv.ShowDialog() == true)
            {

            }
        }
    }
}
