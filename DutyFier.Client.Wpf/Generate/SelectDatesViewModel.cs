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
        public Dictionary<DutyType, List<DateTime>> DatesDutyTypes { get; set; }

        private SelectedDatesCollection selectDates;

        private KeyValuePair<DutyType, List<DateTime>> selectedDutyType;

        private CalendarUI calendarUI;

        public RelayCommand ComadAcpet { get; set; }
        public RelayCommands<SelectedDatesCollection> GetSelectedDatesCollectionComand { get; set; }

        public SelectDatesViewModel(GenerateContext generateContext)
        {
            GetSelectedDatesCollectionComand = new RelayCommands<SelectedDatesCollection>(SelectDateColetion, x => true);
            ComadAcpet = new RelayCommand(SetSelectedDatesForSelectedPosition, CheckActivityButton);
            context = generateContext;
            DatesDutyTypes = generateContext.DutyTypeDate;
            SelectedDutyType = DatesDutyTypes.First();
        }
        public SelectedDatesCollection SelectDates
        {
            get => selectDates;
            set
            {
                selectDates = value;
                OnPropertyChanged(nameof(SelectDates));
            }
        }
        public KeyValuePair<DutyType, List<DateTime>> SelectedDutyType
        {
            get=>selectedDutyType;
            set
            {
                selectedDutyType = value;
                if (calendarUI != null)
                {
                    calendarUI.UpdateClaendar(DatesDutyTypes[selectedDutyType.Key]);
                }
            }
        }

        public GenerateContext context { get; set; }

        private void SelectDateColetion(SelectedDatesCollection obj)
        {
            selectDates = obj;
            calendarUI = new CalendarUI(selectDates);

        }

        private void SetSelectedDatesForSelectedPosition(Object obj)
        {
            DatesDutyTypes[selectedDutyType.Key].Clear();
            DatesDutyTypes[selectedDutyType.Key] = calendarUI.GetSelectedDates();
            selectedDutyType.Value.AddRange(calendarUI.GetSelectedDates());

            DatesDutyTypes = context.DutyTypeDate;
            OnPropertyChanged("DatesPosition");
            OnPropertyChanged("selectPosition");
        }

        private bool CheckActivityButton(Object obj)
        {
            if (selectDates == null)
            {
                return false;
            }
            if (selectedDutyType.Key == null)
            {
                return false;
            }
            foreach (var item in selectDates)
            {
                if (!DatesDutyTypes[selectedDutyType.Key].Contains(item))
                {
                    return true;
                }
            }
            if (selectDates.Count != DatesDutyTypes[selectedDutyType.Key].Count)
            {
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
