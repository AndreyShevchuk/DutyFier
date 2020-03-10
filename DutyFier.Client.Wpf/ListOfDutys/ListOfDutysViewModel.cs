using DutyFier.Core;
using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DutyFier.Client.Wpf.ListOfDutys
{
    class ListOfDutysViewModel
    {
        private FeedbackModel FeedbackModel { get; set; }
        public List<Duty> Dutys { get; set; }
        public ListOfDutysViewModel()
        {
            FeedbackModel = new FeedbackModel(new DutyRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()),
                                            new PersonDutyFeedbackRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
            Dutys = FeedbackModel.GetDutiesWitchHasNoFeedbacks();
        }
    }
}
