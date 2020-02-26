using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Feedback
{
    public class AcceptFeedbackViewModel :INotifyPropertyChanged
    {
        public Duty Duty { get; set; }
        public string Position { get; set; }
        public string Date { get; set; }
        public string DutyType { get; set; }

        private Executor _executor;
        public Executor ChosenExecutor { 
            get=> _executor;
            set 
            {
                _executor = value;
                Position = value.Position.ToString();
                Grade = value.Duty.PreliminaryAssessmentList[Duty.Executors.IndexOf(value)];
                OnPropertyChanged(nameof(Position));
                OnPropertyChanged(nameof(Grade));
            }
        }
        public double Grade { get; set; }
        RelayCommands CanselComand { get; set; }
        RelayCommands AcceptCommand { get; set; }
        RelayCommands AcceptAllCommand { get; set; }
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
            DutyType = duty.Executors[0].Position.DutyType.ToString();
            CanselComand = new RelayCommands(Cansel);
            AcceptCommand = new RelayCommands(CreateFeedback);
            AcceptAllCommand = new RelayCommands(CreateFeedbacksForAll);
            FeedbacksContext = feedbacksContext;
        }

        private void CreateFeedbacksForAll()
        {
            Duty.Executors.ForEach(executor => 
            FeedbacksContext.PersonDutyFeedbacks.Add(
                new PersonDutyFeedback() 
                { 
                    Duty = Duty, 
                    Person = executor.Person, 
                    Source = Duty.PreliminaryAssessmentList[Duty.Executors.IndexOf(executor)]
                })
            );
        }

        private void CreateFeedback()
        {
            FeedbacksContext.PersonDutyFeedbacks.Add(new PersonDutyFeedback()
            {
                Duty = Duty,
                Person = ChosenExecutor.Person,
                Source = Grade
            });
        }

        private void Cansel()
        {
            FeedbacksContext.PersonDutyFeedbacks.Clear();
            //TODO window closings
        }
    }
}
