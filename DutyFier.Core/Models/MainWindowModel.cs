﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DutyFier.Core;

namespace DutyFier.Core.Models
{
    public class MainWindowModel
    {
        private DutyRepository dutyRepository;

        public MainWindowModel(DutyRepository dutyRepository)
        {
            this.dutyRepository = dutyRepository;
        }

        public int GetUncreatedFeedbackCount()=>dutyRepository.GetAll().
                                                    Where(duty => !duty.IsApproved).
                                                    Where(duty=> duty.Date>DateTime.Now.AddDays(1)).
                                                    Count();
    }

}
