using DutyFier.Client.Wpf.State;
using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Generate
{
    class ResultViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

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
            apv.DataContext = new EditDutyViewModel(SelectedDuty, context.GetOnlyAviablePersons(SelectedDuty));
            if (apv.ShowDialog() == true)
            {
                //Dutys = new ObservableCollection<Duty>(context.DutyRepository.GetAll());
                OnPropertyChanged(nameof(Dutys));
                context.Update();
                apv.Close();
            }
            else if (apv.DialogResult == false)
            {
                context.Reload();
                apv.Close();
            }
        }
    }
}
