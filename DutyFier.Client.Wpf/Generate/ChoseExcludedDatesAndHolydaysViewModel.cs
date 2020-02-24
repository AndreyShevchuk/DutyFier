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
    class ChoseExcludedDatesAndHolydaysViewModel
    {
        private Person selctPerson;

        private SelectedDatesCollection selectedDates;

        private CalendarUI CalendarUI;
        public Dictionary<Person, List<DateTime>> ExcludedDates { get; set; }

        public ChoseExcludedDatesAndHolydaysViewModel(GenerateContext generateContext)
        {
            ExcludedDates = generateContext.ExludeDates;
            GetSelectDates = new RelayCommands<SelectedDatesCollection>(GetSelectedDatesColections, x => true);
        }


        public RelayCommands<SelectedDatesCollection> GetSelectDates { get; set; }
        public Person SelcetPerson {
            get 
            {
                return selctPerson; 
            }
            set
            {
                selctPerson = value;
                selectedDates.Clear();

                foreach (var item in ExcludedDates[selctPerson])
                {
                    selectedDates.Add(item);
                }
            }
        }

  
        
        private RelayCommand comandAddExludesDares;
        public RelayCommand ComandAddExludesDares
        {
            get
            {
                return comandAddExludesDares ??
                (comandAddExludesDares = new RelayCommand(obj =>
                {
                    ExcludedDates[SelcetPerson].Clear();
                    foreach (var item in selectedDates)
                    {
                        ExcludedDates[SelcetPerson].Add(item);
                    }
                },
                (obj) => CheckActivityButton()));
            }
        }
        private RelayCommand comandSelectDates;
        public RelayCommand ComandSelectDates
        {
            get
            {
                //return comandSelectDates ??
                //(comandSelectDates = new RelayCommand(obj =>
                //{
                //    if (selectedDates == null)   /// Ініціалізуємо колекцію з календарря щоб могли доступатись з ViewModel  ==> потрыбно xерез xaml ініцалізувати
                //    {
                //        selectedDates = (SelectedDatesCollection)obj;
                //    }
                //    if (SelcetPerson == null) // помилка на дурака
                //    {
                //        MessageBox.Show("Вибери Людину");
                //        return;
                //    };
                //},
                //(obj) => true));
                return null;
            }
            set { 
            }
        }

        private bool CheckActivityButton()
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
        
        private void GetSelectedDatesColections(SelectedDatesCollection obj)
        {
            selectedDates = obj;
            CalendarUI  = new CalendarUI(obj);
        }
    }
}
