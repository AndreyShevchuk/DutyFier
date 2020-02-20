using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

namespace DutyFier.Core.Models
{
    public class StatisticModel
    {
        private  IRepository<Person> PersonRepository { get; set; }
        private IRepository<PersonDutyFeedback> PersonDutyFeedbackRepository { get; set; }

        public StatisticModel(IRepository<Person> personRepository, IRepository<PersonDutyFeedback> personDutyFeedbackRepository)
        {
            PersonRepository = personRepository;
            PersonDutyFeedbackRepository = personDutyFeedbackRepository;
        }

        public List<PersonScoreCover> GetPersons() => PersonScoreCover.GetPersonScoreCoverList(
            PersonRepository.GetAll().ToList(), 
            PersonDutyFeedbackRepository.GetAll().ToList()
            );
    }
}
