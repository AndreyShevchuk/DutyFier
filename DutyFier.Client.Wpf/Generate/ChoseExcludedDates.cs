using DutyFier.Client.Wpf.State;
using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.Generate
{
    class ChoseExcludedDates:INotifyPropertyChanged
    {
        private KeyValuePair<Person, List<DateTime>> selctPerson;

        private SelectedDatesCollection selectedDates;

        private CalendarUI CalendarUI;
        public Dictionary<Person, List<DateTime>> ExcludedDates { get; set; }
        public RelayCommands<SelectedDatesCollection> GetSelectDates { get; set; }
        public RelayCommand ComandAddExludesDares { get; set; }
        public ChoseExcludedDates(GenerateContext generateContext)
        {
            GetSelectDates = new RelayCommands<SelectedDatesCollection>(GetSelectedDatesColections, x => true);
            ComandAddExludesDares = new RelayCommand(SetExcludeDatesForSelectPerson, CheckActivityButton);

            ExcludedDates = generateContext.ExludeDates;
            SelcetPerson = ExcludedDates.First();
        }

        private void GetSelectedDatesColections(SelectedDatesCollection obj)
        {
            selectedDates = obj;
            CalendarUI = new CalendarUI(obj);
        }

        public KeyValuePair<Person, List<DateTime>> SelcetPerson {
            get 
            {
                return selctPerson; 
            }
            set
            {
                selctPerson = value;
                if (CalendarUI != null)
                {
                    CalendarUI.UpdateClaendar(ExcludedDates[selctPerson.Key]);
                }
                OnPropertyChanged("SelcetPerson");
                
            }
        }

        private void SetExcludeDatesForSelectPerson(object obj)
        {
            ExcludedDates[SelcetPerson.Key].Clear();
            ExcludedDates[SelcetPerson.Key] = CalendarUI.GetSelectedDates();
        }

        private bool CheckActivityButton(Object obj)
        {
            if (selectedDates == null)
            {
                return false;
            }
            if (SelcetPerson.Key == null)
            {
                return false;
            }
            foreach (var item in selectedDates)
            {
                if (!ExcludedDates[SelcetPerson.Key].Contains(item))
                {
                    return true;
                }
            }
            if (selectedDates.Count != ExcludedDates[SelcetPerson.Key].Count)
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
