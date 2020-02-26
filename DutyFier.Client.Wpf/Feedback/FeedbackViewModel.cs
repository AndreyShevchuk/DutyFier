using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DutyFier.Client.Wpf.Feedback
{
    public class FeedbackViewModel : INotifyPropertyChanged
    {
        private IRepository<Duty> DutyRepository { get; set; }
        private IRepository<PersonDutyFeedback> PersonDutyFeedbackRepository { get; set; }
        private AcceptFeedbackView AcceptFeedbackView { get; set; }
        public DutyModel SelectedDuty { get; set; }
        FeedbackView.FeedbackChangeCountTrigger FeedbackChangeCount { get; set; }
        public delegate void AcceptFeedbackViewClosingTrigger();
        public AcceptFeedbackViewClosingTrigger ReedFeedbackContext { get; set; }
        RelayCommands CreateAcceptFeedbackViewCommand;
        FeedbacksContext FeedbacksContext { get; set; }
        public DutyModel[] DutyModels
        {
            get => DutyModel.GetDutiesWitchHasNoFeedbacks(DutyRepository);
        }
        public FeedbackViewModel(FeedbackView.FeedbackChangeCountTrigger changeCountTrigger)
        {
            DutyRepository = new DutyRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            PersonDutyFeedbackRepository = new  PersonDutyFeedbackRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            FeedbackChangeCount = changeCountTrigger;
            CreateAcceptFeedbackViewCommand = new RelayCommands(CreateAcceptFeedbackView);
            FeedbacksContext = new FeedbacksContext(new List<PersonDutyFeedback>());
            ReedFeedbackContext = GetFeedbackContext;
        }

        private void GetFeedbackContext()
        {
            FeedbacksContext = AcceptFeedbackView.FeedbacksContext;
        }

        private void CreateAcceptFeedbackView()
        {
            AcceptFeedbackView = new AcceptFeedbackView(DutyRepository.GetAll().Where(duty => duty.Id == SelectedDuty.DutyID).First(), FeedbacksContext, ReedFeedbackContext);
            AcceptFeedbackView.ShowDialog();
            CheackChanges();
        }

        private void CheackChanges()
        {
            if(FeedbacksContext.PersonDutyFeedbacks.Count == SelectedDuty.Persons.Count)
            {
                var selectedDuty = DutyRepository.GetAll().Where(duty => duty.Id == SelectedDuty.DutyID).First();
                selectedDuty.IsApproved = true;
                DutyRepository.Update(selectedDuty);

                PersonDutyFeedbackRepository.AddRange(FeedbacksContext.PersonDutyFeedbacks);
                FeedbackChangeCount?.Invoke();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
