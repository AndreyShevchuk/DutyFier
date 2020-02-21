using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DutyFier.Client.Wpf.Feedback
{
    class FeedbackViewModel
    {
        public List<PersonDutyFeedback> feedbacks;
        private List<Duty> UnapprovedDuties { get; set; }
        public FeedbackViewModel()
        {
            feedbacks = MainWindowViewModel.Container.Resolve<IRepository<PersonDutyFeedback>>().GetAll().ToList();
            UnapprovedDuties = MainWindowViewModel.Container.Resolve<IRepository<Duty>>().GetAll().Where(a=> !a.IsApproved).ToList();
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
