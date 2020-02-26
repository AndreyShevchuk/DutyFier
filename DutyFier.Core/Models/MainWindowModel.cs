using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DutyFier.Core.Repository;

namespace DutyFier.Core.Models
{
    public class MainWindowModel
    {
        private DutyRepository dutyRepository;

        public MainWindowModel(DutyRepository dutyRepository)
        {
            this.dutyRepository = dutyRepository;
        }

        public int GetUncreatedFeedbackCount()=>dutyRepository.GetAll().Where(duty => !duty.IsApproved).Count();
    }

}
