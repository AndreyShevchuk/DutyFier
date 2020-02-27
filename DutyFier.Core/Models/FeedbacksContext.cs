using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Models
{
    public class FeedbacksContext
    {
        public List<PersonDutyFeedback> PersonDutyFeedbacks { get; set; }

        public FeedbacksContext(List<PersonDutyFeedback> personDutyFeedbacks)
        {
            PersonDutyFeedbacks = personDutyFeedbacks;
        }
    }
}
