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
        public DutyRequest SelectDutyReqest {
            get
            {
                return selectDutyReqest;
            }
            set
            {
                selectDutyReqest = value;
                OnPropertyChanged("DutyRequests");
            }
        }

        public PreViewModel(GenerateContext context)
        {
            DutyRequests = context.DutyRequests;
            PlusDutyComand = new RelayCommands<DutyRequest>(PlusDuty, x => true);
            MinusDutyComand = new RelayCommands<DutyRequest>(MinusDuty, ActitvityMinusButton);
        }

        public RelayCommands<DutyRequest> PlusDutyComand { get; set; }
        public RelayCommands<DutyRequest> MinusDutyComand { get; set; }

        public void PlusDuty(DutyRequest obj)
        {
            SelectDutyReqest.Positions.Add(SelectDutyReqest.Positions[0]);
            OnPropertyChanged("DutyRequests");
        }
        public void MinusDuty(DutyRequest obj)
        {
            SelectDutyReqest.Positions.RemoveAt(SelectDutyReqest.Positions.Count - 1); // Remove
            OnPropertyChanged("DutyRequests");
        }
        public bool ActitvityMinusButton(DutyRequest obj)
        {
            if (SelectDutyReqest == null)
            {
                return true;
            }
            if (SelectDutyReqest.Positions.Count <= 1)
            {
                return false;
            }
            return true;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
