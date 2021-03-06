﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DutyFier.Core.Entities;
using DutyFier.Core;

namespace DutyFier.Core.Models
{
    public class FeedbackModel
    {
        private DutyRepository DutyRepository { get; set; }
        private PersonDutyFeedbackRepository PersonDutyFeedbackRepository { get; set; }

        public FeedbackModel(DutyRepository dutyRepository, PersonDutyFeedbackRepository personDutyFeedbackRepository)
        {
            this.DutyRepository = dutyRepository;
            this.PersonDutyFeedbackRepository = personDutyFeedbackRepository;
        }

        public void CreateDutyFeedbacksFromDutyModelAndContext(Duty selectedDuty, FeedbacksContext feedbacksContext)
        {
            selectedDuty.IsApproved = true;
            DutyRepository.Update(selectedDuty);
            PersonDutyFeedbackRepository.AddRange(feedbacksContext.PersonDutyFeedbacks);
        }

        public List<Duty> GetDutiesWitchHasNoFeedbacks() => GetDutiesWitchHasNoFeedbacks(DutyRepository);

        private List<Duty> GetDutiesWitchHasNoFeedbacks(DutyRepository dutyRepository)=>dutyRepository.GetAll().Where(duty => !duty.IsApproved).ToList();
        public List<Duty> GetDuties() => GetDuties(DutyRepository);

        private List<Duty> GetDuties(DutyRepository dutyRepository)
        {
            var dutys = dutyRepository.GetAll().ToList().ToList();
            dutys.Sort((a, b) => a.Date.CompareTo(b.Date));
            return dutys;
        }
        public void CreateDutyFeedbacksFromDutyModelWithDefauld(Duty selectedDuty)
        {
            selectedDuty.IsApproved = true;
            var feedbacks = selectedDuty.Executors.Select(ex => new PersonDutyFeedback()
            {
                Duty = selectedDuty,
                Person = ex.Person,
                Score = ex.PreliminaryScore

            }).ToList();

            PersonDutyFeedbackRepository.AddRange(feedbacks);
        }
    }
}
