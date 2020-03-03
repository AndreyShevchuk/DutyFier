using DutyFier.Client.Wpf.State;
using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using DutyFier.Core.Models;
using System.Linq;

namespace DutyFier.Client.Wpf.Generate
{
    class SelectDatesViewModel : INotifyPropertyChanged
    {
        public Dictionary<Position, List<DateTime>> DatesPosition { get => datesPosition; set => datesPosition = value; }

        private SelectedDatesCollection selectDates;

        private KeyValuePair<Position, List<DateTime>> selectPosition;

        private CalendarUI calendarUI;
        private Dictionary<Position, List<DateTime>> datesPosition;

        public RelayCommand ComadAcpet { get; set; }
        public RelayCommands<SelectedDatesCollection> GetSelectedDatesCollectionComand { get; set; }

        public SelectDatesViewModel(GenerateContext generateContext)
        {
            GetSelectedDatesCollectionComand = new RelayCommands<SelectedDatesCollection>(SelectDateColetion, x => true);
            ComadAcpet = new RelayCommand(SetSelectedDatesForSelectedPosition, CheckActivityButton);

            DatesPosition = generateContext.PositionsDate;
            SelectPosition = DatesPosition.First();
        }
        public SelectedDatesCollection SelectDates
        {
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
        public KeyValuePair<Position, List<DateTime>> SelectPosition
        {
            get
            {
                return selectPosition;
            }
            set
            {
                selectPosition = value;
                if (calendarUI != null)
                {
                    calendarUI.UpdateClaendar(DatesPosition[selectPosition.Key]);
                }
            }
        }
        private void SelectDateColetion(SelectedDatesCollection obj)
        {
            selectDates = obj;
            calendarUI = new CalendarUI(selectDates);
        }

        private void SetSelectedDatesForSelectedPosition(Object obj)
        {
            DatesPosition[selectPosition.Key].Clear();
            DatesPosition[selectPosition.Key] = calendarUI.GetSelectedDates();
            selectPosition.Value.AddRange(calendarUI.GetSelectedDates());
            
            OnPropertyChanged("DatesPosition");
            OnPropertyChanged("SelectPosition");
        }

        private bool CheckActivityButton(Object obj)
        {
            if (selectDates == null)
            {
                return false;
            }
            if (selectPosition.Key == null)
            {
                return false;
            }
            foreach (var item in selectDates)
            {
                if (!DatesPosition[selectPosition.Key].Contains(item))
                {
                    return true;
                }
            }
            if (selectDates.Count != DatesPosition[selectPosition.Key].Count)
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
