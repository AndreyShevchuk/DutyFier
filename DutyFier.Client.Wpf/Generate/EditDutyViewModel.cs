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
    class EditDutyViewModel : INotifyPropertyChanged
    {
        private Duty selectedDuty;
        public Duty ChangeDuty { get;
            set; }
        public ObservableCollection<Person> FullPersonsDuty { get; set; }
        public ObservableCollection<Person> FullPersons { get; set; }

        private ObservableCollection<MyDictionary> test;

        public ObservableCollection<MyDictionary> Test
        {
            get
            {
                return test;
            }
            set
            {
                test = value;
            }
        }

        public EditDutyViewModel(Duty duty, IEnumerable<Person> persons)
        {
            ChangeDuty = duty;
            FullPersonsDuty = new ObservableCollection<Person>(duty.Executors.Select(e => e.Person));
            FullPersons = new ObservableCollection<Person>(persons);
            MetodTest(FullPersonsDuty, FullPersons);
        }
        private void MetodTest(ICollection<Person> key, ObservableCollection<Person> value)
        {
            Test = new ObservableCollection<MyDictionary>();
            foreach (var item in key)
            {
                Test.Add( new MyDictionary( item, value));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    class MyDictionary
    {
        public Person Key { get; 
            set; }
        public ObservableCollection<Person> Value { get; set; }

        public MyDictionary(Person key, ObservableCollection<Person> values)
        {
            Key = key;
            Value = values;
        }
    }
}
