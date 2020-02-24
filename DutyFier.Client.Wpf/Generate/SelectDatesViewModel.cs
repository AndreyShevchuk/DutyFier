using DutyFier.Client.Wpf.State;
using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using DutyFier.Core.Models;
using System.Linq;

namespace DutyFier.Client.Wpf.Generate
{
    class SelectDatesViewModel : INotifyPropertyChanged
    {
        public Dictionary<Position, List<DateTime>> DatesPosition { get; set; }

        private SelectedDatesCollection selectDates;

        private Position selectPosition;

        private CalendarUI calendarUI;
        public RelayCommand ComadAcpet { get; set; }
        public RelayCommands<SelectedDatesCollection> GetSelectedDatesCollectionComand { get; set; }

        public SelectDatesViewModel(GenerateContext generateContext)
        {
            GetSelectedDatesCollectionComand = new RelayCommands<SelectedDatesCollection>(SelectDateColetion, x => SelectPosition != null);
            ComadAcpet = new RelayCommand(SetSelectedDatesForSelectedPosition, CheckActivityButton);

            DatesPosition = generateContext.PositionsDate;
            SelectPosition = DatesPosition.Keys.First();
        }

        private void SelectDateColetion(SelectedDatesCollection obj)
        {
            selectDates = obj;
            calendarUI = new CalendarUI(selectDates);
        }

        private void SetSelectedDatesForSelectedPosition(Object obj)
        {
            DatesPosition[selectPosition].Clear();
            DatesPosition[selectPosition] = calendarUI.GetSelectedDates();
        }

        private bool CheckActivityButton(Object obj)
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
            if (selectDates.Count != DatesPosition[selectPosition].Count)
            {
                return true;
            }
            return false;
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
        public Position SelectPosition
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
                    calendarUI.UpdateClaendar(DatesPosition[selectPosition]);
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
