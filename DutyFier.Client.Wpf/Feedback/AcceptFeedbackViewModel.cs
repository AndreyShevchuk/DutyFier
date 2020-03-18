using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DutyFier.Client.Wpf.Feedback
{
    public class AcceptFeedbackViewModel : INotifyPropertyChanged
    {
        public Duty Duty { get; set; }
        public string Position { get; set; }
        public string Date { get; set; }
        public string DutyType { get; set; }
        public AcceptFeedbackModel AcceptFeedbackModel { get; set; }
        private Person _person;
        public ObservableCollection<Person> People {get;set;}

        public bool IsLast 
        {
            get => People.Count == 1;
        }
        public Person SelectedPerson { 
            get=> _person;
            set 
            {
                if (value != null)
                {
                    _person = value;
                    Position = Duty.Executors.Find(ex => ex.Person.Equals(_person)).Position.ToString();
                    Grade = Duty.Executors.Find(ex => ex.Person.Equals(_person)).PreliminaryScore;
                    OnPropertyChanged(nameof(Position));
                    OnPropertyChanged(nameof(Grade));
                }
            }
        }
        public double Grade { get; set; }
        public RelayCommands CanselComand { get; set; }
        public RelayCommands AcceptCommand { get; set; }
        public RelayCommands AcceptAllCommand { get; set; }
        FeedbacksContext FeedbacksContext { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {   
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public AcceptFeedbackViewModel(Duty duty, FeedbacksContext feedbacksContext)
        {
            Duty = duty;
            Date = duty.Date.ToShortDateString();
            AcceptFeedbackModel = new AcceptFeedbackModel();
            People = new ObservableCollection<Person>(duty.Executors.Select(ex => ex.Person));
            DutyType = duty.Executors[0].Position.DutyType.ToString();
            CanselComand = new RelayCommands(Cansel);
            AcceptCommand = new RelayCommands(CreateFeedback);
            AcceptAllCommand = new RelayCommands(CreateFeedbacksForAll);
            FeedbacksContext = feedbacksContext;
            SelectedPerson = People.First();
        }

        private void CreateFeedbacksForAll()
        {
            FeedbacksContext.PersonDutyFeedbacks.AddRange( AcceptFeedbackModel.CreateFeedbacksForAll(Duty));
        }

        private void CreateFeedback()
        {

            FeedbacksContext.PersonDutyFeedbacks.Add(AcceptFeedbackModel.CreateFeedback(Duty, Duty.Executors.Find(ex => ex.Person.Equals(SelectedPerson)), Grade));
            People.Remove(SelectedPerson);
            if(People.Count>0)
            SelectedPerson = People.First();
            OnPropertyChanged("SelectePerson");
            OnPropertyChanged(nameof(IsLast));
            OnPropertyChanged("Executors");
        }

        private void Cansel()
        {
            FeedbacksContext.PersonDutyFeedbacks.Clear();
            //TODO window closings
        }
    }
}
