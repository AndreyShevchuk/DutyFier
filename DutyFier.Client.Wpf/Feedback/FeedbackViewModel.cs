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
using System.Collections.ObjectModel;

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
        public RelayCommands CreateAcceptFeedbackViewCommand { get; set; }
        public RelayCommands AcceptAllCommand { get; set; }
        FeedbacksContext FeedbacksContext { get; set; }
        public ObservableCollection<Duty> Dutys { get; set; }
        public FeedbackViewModel(FeedbackView.FeedbackChangeCountTrigger changeCountTrigger)
        {
            FeedbackChangeCount = changeCountTrigger;
            CreateAcceptFeedbackViewCommand = new RelayCommands(createAcceptFeedbackView, ()=> true);
            AcceptAllCommand = new RelayCommands(AcceptAll,()=>true);
            FeedbacksContext = new FeedbacksContext(new List<PersonDutyFeedback>());
            ReedFeedbackContext = GetFeedbackContext;
            FeedbackModel = new FeedbackModel(new DutyRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()),
                                            new PersonDutyFeedbackRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));

            Dutys = new ObservableCollection<Duty>(FeedbackModel.GetDutiesWitchHasNoFeedbacks());
        }

        private void AcceptAll()
        {
            Dutys.ToList().ForEach(a => FeedbackModel.CreateDutyFeedbacksFromDutyModelWithDefauld(a));
            Dutys.Clear();
        }

        private void GetFeedbackContext()
        {
            FeedbacksContext = AcceptFeedbackView.FeedbacksContext;
        }

       private void createAcceptFeedbackView()
        {
            AcceptFeedbackView = new AcceptFeedbackView( SelectedDuty, FeedbacksContext, ReedFeedbackContext);

            if(AcceptFeedbackView.ShowDialog() == true)
            {

                CheackChanges();
                AcceptFeedbackView.Close();
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
