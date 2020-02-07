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
    class SelectDatesViewModel : INotifyPropertyChanged
    {
        public Dictionary<Position, ObservableCollection<DateTime>> DatesPosition { get; set; }

        private ObservableCollection<DateTime> selectDates;
        public ObservableCollection<DateTime> SelectDates { 
            get 
            {
                return selectDates;
            }
            set 
            {
                selectDates = value;
                OnPropertyChanged("SelectDates");
            }
        }

        private Position selectPosition;
        public Position SelectPosition
        {
            get
            {
                return selectPosition;
            }
            set
            {
                selectPosition = value;
                OnPropertyChanged("SelectPosition");
                //out ObservableCollection<DateTime> temp;
                //DatesPosition.TryGetValue(value,out temp);
                //SelectDates = temp;
            }
        }


        public SelectDatesViewModel()
        {
            DatesPosition = new Dictionary<Position, ObservableCollection<DateTime>>();
            DatesPosition.Add(new Position() { Name = "Kpp" }, new ObservableCollection<DateTime>());
            DatesPosition.Add(new Position() { Name = "Tsp" }, new ObservableCollection<DateTime>());
            DatesPosition.Add(new Position() { Name = "isi2" }, new ObservableCollection<DateTime>());
            DatesPosition.Add(new Position() { Name = "isi3" }, new ObservableCollection<DateTime>());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
