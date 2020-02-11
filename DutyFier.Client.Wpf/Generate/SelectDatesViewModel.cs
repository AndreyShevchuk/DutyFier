using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.Generate
{
    class SelectDatesViewModel : INotifyPropertyChanged
    {
        public Dictionary<Position, ObservableCollection<DateTime>> DatesPosition { get; set; }

        private SelectedDatesCollection selectDates;

        private RelayCommand comadAcpet;
        public RelayCommand ComadAcpet
        {
            get
            {
                return comadAcpet ??
                (comadAcpet = new RelayCommand(obj =>
                {
                    DatesPosition[selectPosition].Clear();
                    foreach (var item in selectDates)
                    {
                        DatesPosition[selectPosition].Add(item);
                    }
                },
                (obj) => CheckActivityButton()));
            }
        }

        private  bool CheckActivityButton()
        {
            if (selectDates == null)
            {
                return false;
            }
            if (selectPosition == null)
            {
                return false;
            }
            foreach (var item in selectDates)
            {
                if (!DatesPosition[selectPosition].Contains(item))
                {
                    return true;
                }
            }
            if(selectDates.Count != DatesPosition[selectPosition].Count)
            {
                return true;
            }
            return false;
        }

        private RelayCommand comandSelectDates;
        public RelayCommand ComandSelectDates
        {
            get
            {
                  return comandSelectDates ??
                  (comandSelectDates = new RelayCommand(obj =>
                  {
                      if (selectDates == null)   /// Ініціалізуємо колекцію з календарря щоб могли доступатись з ViewModel  ==> потрыбно xерез xaml ініцалізувати
                      {
                          selectDates = (SelectedDatesCollection)obj;
                      }
                      if (selectPosition == null) // помилка на дурака
                      {
                          MessageBox.Show("Вибери тип наояду");
                          return;
                      };
                  },
                  (obj) => true));
            }
        }
        public SelectedDatesCollection SelectDates { 
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

                if (selectDates != null)
                {
                    selectDates.Clear();
                }

                if (DatesPosition[selectPosition] == null)
                {
                    DatesPosition[selectPosition] = new ObservableCollection<DateTime>();
                }
                else
                {
                    foreach (var item in DatesPosition[selectPosition])
                    {
                        if (selectDates != null)
                        {
                            selectDates.Add(item);
                        }
                    }
                }
            }
        }

        public SelectDatesViewModel()
        {
            DatesPosition = new Dictionary<Position, ObservableCollection<DateTime>>();
            DatesPosition.Add(new Position() { Name = "Kpp" }, null);
            DatesPosition.Add(new Position() { Name = "Tsp" }, null);
            DatesPosition.Add(new Position() { Name = "isi2" }, null);
            DatesPosition.Add(new Position() { Name = "isi3" }, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
