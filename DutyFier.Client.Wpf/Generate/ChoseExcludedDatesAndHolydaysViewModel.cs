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
        
        private SelectedDatesCollection selectDates;

        public Person SelcetPerson {
            get 
            {
                return selctPerson; 
            }
            set
            {
                selctPerson = value;

                if (selectDates != null)
                {
                    selectDates.Clear();
                }

                if (ExcludedDates[selctPerson] == null)
                {
                    ExcludedDates[selctPerson] = new ObservableCollection<DateTime>();
                }
                else
                {
                    foreach (var item in ExcludedDates[selctPerson])
                    {
                        if (selectDates != null)
                        {
                            selectDates.Add(item);
                        }
                    }
                }

            }
        }

        public SelectedDatesCollection SelectedDates { get { return selectDates; } set { selectDates = value; } }
        public Dictionary<Person, ObservableCollection<DateTime>> ExcludedDates { get; set; }     
        
        private RelayCommand comandAddExludesDares;
        public RelayCommand ComandAddExludesDares
        {
            get
            {
                return comandAddExludesDares ??
                (comandAddExludesDares = new RelayCommand(obj =>
                {
                    ExcludedDates[SelcetPerson].Clear();
                    foreach (var item in SelectedDates)
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
                return comandSelectDates ??
                (comandSelectDates = new RelayCommand(obj =>
                {
                    if (SelectedDates == null)   /// Ініціалізуємо колекцію з календарря щоб могли доступатись з ViewModel  ==> потрыбно xерез xaml ініцалізувати
                    {
                        SelectedDates = (SelectedDatesCollection)obj;
                    }
                    if (SelcetPerson == null) // помилка на дурака
                    {
                        MessageBox.Show("Вибери Людину");
                        return;
                    };
                },
                (obj) => true));
            }
        }

        private bool CheckActivityButton()
        {
            if (SelectedDates == null)
            {
                return false;
            }
            if (SelcetPerson == null)
            {
                return false;
            }
            foreach (var item in SelectedDates)
            {
                if (!ExcludedDates[SelcetPerson].Contains(item))
                {
                    return true;
                }
            }
            if (SelectedDates.Count != ExcludedDates[SelcetPerson].Count)
            {
                return true;
            }
            return false;
        }
        public ChoseExcludedDatesAndHolydaysViewModel()
        {
            
            ExcludedDates = new Dictionary<Person, ObservableCollection<DateTime>>();
            ExcludedDates.Add(new Person { FirstName = "Andrey1", Id = 1 }, new ObservableCollection<DateTime>());
            ExcludedDates.Add(new Person { FirstName = "Andrey2", Id = 2 }, new ObservableCollection<DateTime>());
            ExcludedDates.Add(new Person { FirstName = "Andrey3", Id = 3 }, new ObservableCollection<DateTime>());
            ExcludedDates.Add(new Person { FirstName = "Andrey4", Id = 4 }, new ObservableCollection<DateTime>());
        }
    }
}
