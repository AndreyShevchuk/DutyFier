using DutyFier.Core.Entities;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Feedback
{
    class FeedbackViewModel
    {
        public List<PersonDutyFeedback> feedbacks;
        public FeedbackViewModel()
        {
            Feedbacks = new PersonDutyFeedbackRepository().GetAll().ToList();
        }
        public List<PersonDutyFeedback> Feedbacks
        {
            get
            {
                return feedbacks;
            }
            set
            {
                feedbacks = value;
            }
        }
    }
}
