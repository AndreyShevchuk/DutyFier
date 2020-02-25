using DutyFier.Client.Wpf.State;
using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.Generate
{
    class ChoseExcludedDates
    {
        private Person selctPerson;

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
            SelcetPerson = ExcludedDates.Keys.First();
        }

        private void GetSelectedDatesColections(SelectedDatesCollection obj)
        {
            selectedDates = obj;
            CalendarUI = new CalendarUI(obj);
        }

        public Person SelcetPerson {
            get 
            {
                return selctPerson; 
            }
            set
            {
                selctPerson = value;
                if (CalendarUI != null)
                {
                    CalendarUI.UpdateClaendar(ExcludedDates[selctPerson]);
                }
            }
        }

        private void SetExcludeDatesForSelectPerson(object obj)
        {
            ExcludedDates[SelcetPerson].Clear();
            ExcludedDates[SelcetPerson] = CalendarUI.GetSelectedDates();
        }

        private bool CheckActivityButton(Object obj)
        {
            if (selectedDates == null)
            {
                return false;
            }
            if (SelcetPerson == null)
            {
                return false;
            }
            foreach (var item in selectedDates)
            {
                if (!ExcludedDates[SelcetPerson].Contains(item))
                {
                    return true;
                }
            }
            if (selectedDates.Count != ExcludedDates[SelcetPerson].Count)
            {
                return true;
            }
            return false;
        }
    }
}
