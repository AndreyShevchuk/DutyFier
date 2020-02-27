using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DutyFier.Core.Entities;
using DutyFier.Core.Repository;

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

        private List<Duty> GetDutiesWitchHasNoFeedbacks(DutyRepository dutyRepository)=>dutyRepository.GetAll().Where(duty => duty.Date.AddDays(1) < DateTime.Now).Where(duty => !duty.IsApproved).ToList();

        public void CreateDutyFeedbacksFromDutyModelWithDefauld(Duty selectedDuty)
        {
            selectedDuty.IsApproved = true;
            var feedbacks = selectedDuty.Executors.Select(ex => new PersonDutyFeedback()
            {
                Duty = selectedDuty,
                Person = ex.Person,
                Source = selectedDuty.PreliminaryAssessmentList[selectedDuty.Executors.IndexOf(ex)]

            }).ToList();

            PersonDutyFeedbackRepository.AddRange(feedbacks);
        }
    }
}
