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

        public Duty GetDutyFromDutyModel(DutyModel selectedDuty)=> DutyRepository.GetAll().Where(duty => duty.Id == selectedDuty.DutyID).First();

        public void CreateDutyFeedbacksFromDutyModelAndContext(DutyModel selectedDutyModel, FeedbacksContext feedbacksContext)
        {
            var selectedDuty = GetDutyFromDutyModel(selectedDutyModel);
            selectedDuty.IsApproved = true;
            DutyRepository.Update(selectedDuty);
            PersonDutyFeedbackRepository.AddRange(feedbacksContext.PersonDutyFeedbacks);
        }

        public DutyModel[] GetDutiesWitchHasNoFeedbacks() => DutyModel.GetDutiesWitchHasNoFeedbacks(DutyRepository);

    }
}
