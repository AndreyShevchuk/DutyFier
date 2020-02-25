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
        public RelayCommands<DutyRequest> PlusDutyComand { get; set; }
        public RelayCommands<DutyRequest> MinusDutyComand { get; set; }
        public RelayCommands GenerateDuty { get; set; }
        public DutyRequest SelectDutyReqest {
            get
            {
                return selectDutyReqest;
            }
            set
            {
                selectDutyReqest = value;
                OnPropertyChanged("SelectDutyReqest");
            }
        }
        public PreViewModel(GenerateContext context)
        {
            DutyRequests = context.DutyRequests;
            GenerateDuty = new RelayCommands(context.GeneratorRun, () => true);
            PlusDutyComand = new RelayCommands<DutyRequest>(PlusDuty, x => true);
            MinusDutyComand = new RelayCommands<DutyRequest>(MinusDuty, x => true);
        }

        public void PlusDuty(DutyRequest obj)
        {
            SelectDutyReqest.Positions.Add(SelectDutyReqest.Positions[0]);
            var index = DutyRequests.IndexOf(SelectDutyReqest);
            DutyRequests.Insert(index, SelectDutyReqest);
            DutyRequests.RemoveAt(index+1);
        }
        public void MinusDuty(DutyRequest obj)
        {
            SelectDutyReqest.Positions.RemoveAt(SelectDutyReqest.Positions.Count - 1); // Remove
            var index = DutyRequests.IndexOf(SelectDutyReqest);
            DutyRequests.Insert(index, SelectDutyReqest);
            DutyRequests.RemoveAt(index + 1);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
