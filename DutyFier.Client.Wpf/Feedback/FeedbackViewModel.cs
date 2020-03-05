using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using DutyFier.Core;
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
    public class FeedbackViewModel
    {
        private FeedbackModel FeedbackModel { get; set; }
        private AcceptFeedbackView AcceptFeedbackView { get; set; }
        public Duty SelectedDuty { get; set; }
        FeedbackView.FeedbackChangeCountTrigger FeedbackChangeCount { get; set; }
        public delegate void AcceptFeedbackViewClosingTrigger();
        public AcceptFeedbackViewClosingTrigger ReedFeedbackContext { get; set; }
        RelayCommands CreateAcceptFeedbackViewCommand;
        RelayCommands AcceptAllCommand;
        FeedbacksContext FeedbacksContext { get; set; }
        public List<Duty> Dutys
        {
            get => FeedbackModel.GetDutiesWitchHasNoFeedbacks();
        }
        public FeedbackViewModel(FeedbackView.FeedbackChangeCountTrigger changeCountTrigger)
        {
            FeedbackChangeCount = changeCountTrigger;
            CreateAcceptFeedbackViewCommand = new RelayCommands(CreateAcceptFeedbackView);
            AcceptAllCommand = new RelayCommands(AcceptAll);
            FeedbacksContext = new FeedbacksContext(new List<PersonDutyFeedback>());
            ReedFeedbackContext = GetFeedbackContext;
            FeedbackModel = new FeedbackModel(new DutyRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()),
                                            new PersonDutyFeedbackRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
        }

        private void AcceptAll()
        {
            Dutys.ForEach(a => FeedbackModel.CreateDutyFeedbacksFromDutyModelWithDefauld(a));
        }

        private void GetFeedbackContext()
        {
            FeedbacksContext = AcceptFeedbackView.FeedbacksContext;
        }

        private void CreateAcceptFeedbackView()
        {
            AcceptFeedbackView = new AcceptFeedbackView( SelectedDuty, FeedbacksContext, ReedFeedbackContext);

            if(AcceptFeedbackView.ShowDialog() == true)
            {

                CheackChanges();
            }
        }

        private void CheackChanges()
        {
            if(FeedbacksContext.PersonDutyFeedbacks.Count == SelectedDuty.Executors.Count)
            {
                FeedbackModel.CreateDutyFeedbacksFromDutyModelAndContext(SelectedDuty, FeedbacksContext);
                FeedbackChangeCount?.Invoke();
            }
        }
    }
}
