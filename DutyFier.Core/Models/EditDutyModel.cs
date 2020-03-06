using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Models
{
    class EditDutyModel
    {
        private IRepository<Duty> dutyRepository { get; set; }

        private Duty duty;

        public EditDutyModel(IRepository<Duty> personRepository)
        {
            dutyRepository = personRepository;
        }

        //private void AddExecutor()
        //{
        //    duty.Executors.Add(new Executor());
        //}

        private void RemuveExecutor(Executor executor)
        {
            duty.Executors.RemoveAt(duty.Executors.IndexOf(executor));
        }

        private void SaveChange()
        {
            dutyRepository.Update(duty);
        }

    }
}
