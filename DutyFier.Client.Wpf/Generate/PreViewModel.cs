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
    class PreViewModel : INotifyPropertyChanged
    {
        private DutyRequest selectDutyReqest;
        public ObservableCollection<DutyRequest> DutyRequests { get; set; }
        public RelayCommands PlusDutyComand { get; set; }
        public RelayCommands MinusDutyComand { get; set; }
        public RelayCommands GenerateDuty { get; set; }
        public DutyRequest SelectDutyReqest
        {
            get => selectDutyReqest;
            set
            {
                selectDutyReqest = value;
                OnPropertyChanged(nameof(SelectDutyReqest));
            }
        }
        public PreViewModel(GenerateContext context)
        {
            DutyRequests = context.DutyRequests;
            GenerateDuty = new RelayCommands(context.GeneratorRun, () => true);
            PlusDutyComand = new RelayCommands(PlusDuty, () => true);
            MinusDutyComand = new RelayCommands(MinusDuty, () => true);
        }

        public void PlusDuty()
        {
            SelectDutyReqest.Positions.Add(SelectDutyReqest.Positions.First());
        }
        public void MinusDuty()
        {
            if (SubtractionIsPossible())
            {
                SelectDutyReqest.Positions.Remove(SelectDutyReqest.Positions.Last()); // Remove
            }
        }

        private bool SubtractionIsPossible()
        {
            if (SelectDutyReqest.Positions.Count > 1)
            {
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
