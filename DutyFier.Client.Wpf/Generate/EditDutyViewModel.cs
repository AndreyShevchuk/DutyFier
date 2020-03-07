using DutyFier.Core;
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
using Unity;

namespace DutyFier.Client.Wpf.Generate
{
    class EditDutyViewModel : INotifyPropertyChanged
    {
        public Duty ChangeDuty { get; set; }
        public ObservableCollection<Person> FullPersonsDuty { get; set; }
        public ObservableCollection<Person> FullPersons { get; set; }

        private ObservableCollection<MyDictionary> test;

        public RelayCommands AddPersonComand { get; set; }
        public RelayCommands<MyDictionary> DelPersonComand { get; set; }
        public RelayCommands OkComand { get; set; }

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
            AddPersonComand = new RelayCommands(AddExecutor, () => true);
            DelPersonComand = new RelayCommands<MyDictionary>(RemuveExecutor, obj => true);
            OkComand = new RelayCommands(ChangeExecutor, () => true);
        }

        private void RemuveExecutor(MyDictionary myDictionary)
        {
            Test.Remove(myDictionary);
            var delEx = ChangeDuty.Executors.Find(ex => ex.Person.Equals(myDictionary.Key));
            new ExecuterRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()).Delete(delEx);
            ChangeDuty.Executors.Remove(delEx);
        }
        private void AddExecutor()
        {
            Test.Add(new MyDictionary(new Person(), FullPersons));
            ChangeDuty.Executors.Add(new Executor { Position = ChangeDuty.Executors.Count>0? ChangeDuty.Executors.First().Position: null, Duty = ChangeDuty});
        }

        private void ChangeExecutor()
        {
            var tresh = ChangeDuty.Executors[0].Position;
           // ChangeDuty.Executors.Clear();  
            for (int i = 0; i < ChangeDuty.Executors.Count; i++)
            {
                ChangeDuty.Executors[i].Person = Test.ElementAt(i).Key;
            }

            //foreach (var item in Test)
            //{
            //    if(item != null)
            //    {
            //        ChangeDuty.Executors.Add(new Executor() {Id =10000, Person = item.Key, Position = tresh , Duty = ChangeDuty, DutyId = ChangeDuty.Id });
            //    }
            //}
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
        public Person Key { get; set; }
        public ObservableCollection<Person> Value { get; set; }
        public MyDictionary(Person key, ObservableCollection<Person> values)
        {
            Key = key;
            Value = values;
        }
    }
}
