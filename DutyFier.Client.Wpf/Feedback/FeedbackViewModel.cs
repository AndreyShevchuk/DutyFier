﻿using DutyFier.Core.Entities;
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
    public class FeedbackViewModel
    {
        private FeedbackModel FeedbackModel { get; set; }
        private AcceptFeedbackView AcceptFeedbackView { get; set; }
        public DutyModel SelectedDuty { get; set; }
        FeedbackView.FeedbackChangeCountTrigger FeedbackChangeCount { get; set; }
        public delegate void AcceptFeedbackViewClosingTrigger();
        public AcceptFeedbackViewClosingTrigger ReedFeedbackContext { get; set; }
        RelayCommands CreateAcceptFeedbackViewCommand;
        FeedbacksContext FeedbacksContext { get; set; }
        public DutyModel[] DutyModels
        {
            get => FeedbackModel.GetDutiesWitchHasNoFeedbacks();
        }
        public FeedbackViewModel(FeedbackView.FeedbackChangeCountTrigger changeCountTrigger)
        {
            FeedbackChangeCount = changeCountTrigger;
            CreateAcceptFeedbackViewCommand = new RelayCommands(CreateAcceptFeedbackView);
            FeedbacksContext = new FeedbacksContext(new List<PersonDutyFeedback>());
            ReedFeedbackContext = GetFeedbackContext;
            FeedbackModel = new FeedbackModel(new DutyRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()),
                                            new PersonDutyFeedbackRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
        }

        private void GetFeedbackContext()
        {
            FeedbacksContext = AcceptFeedbackView.FeedbacksContext;
        }

        private void CreateAcceptFeedbackView()
        {
            AcceptFeedbackView = new AcceptFeedbackView( FeedbackModel.GetDutyFromDutyModel(SelectedDuty), FeedbacksContext, ReedFeedbackContext);
            AcceptFeedbackView.ShowDialog();
            CheackChanges();
        }

        private void CheackChanges()
        {
            if(FeedbacksContext.PersonDutyFeedbacks.Count == SelectedDuty.Persons.Count)
            {
                FeedbackModel.CreateDutyFeedbacksFromDutyModelAndContext(SelectedDuty, FeedbacksContext);
                FeedbackChangeCount?.Invoke();
            }
        }
    }
}
