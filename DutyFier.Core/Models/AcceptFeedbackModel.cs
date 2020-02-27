using System;
using System.Collections.Generic;
using System.Text;
using DutyFier.Core.Entities;

namespace DutyFier.Core.Models
{
    public class AcceptFeedbackModel
    {
        public IEnumerable<PersonDutyFeedback> CreateFeedbacksForAll(Duty duty)
        {
            var personDutyFeedbacks = new List<PersonDutyFeedback>();
            duty.Executors.ForEach(executor =>
            personDutyFeedbacks.Add(
                new PersonDutyFeedback()
                {
                    Duty = duty,
                    Person = executor.Person,
                    Source = duty.PreliminaryAssessmentList[duty.Executors.IndexOf(executor)]
                })
            );

            return personDutyFeedbacks;
        }

        public PersonDutyFeedback CreateFeedback(Duty duty, Executor chosenExecutor, double grade)
        {
            return new PersonDutyFeedback()
            {
                Duty = duty,
                Person = chosenExecutor.Person,
                Source = grade
            };
        }

        public AcceptFeedbackModel() {}
    }
}
