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

        public List<Person> GetPersons()
        {
            var persons = PersonRepository.GetAll().ToList();
            var feedbacks = PersonDutyFeedbackRepository.GetAll().ToList();
            var personScoreCovers = new List<PersonScoreCover>();
            double score;
            for (int i = 0; i < persons.Count; i++)
            {
                
                score = feedbacks.
                    Where(a => a.Person.Equals(persons[i])).
                    Select(a => a.Source).
                    Sum();
                personScoreCovers.Add(new PersonScoreCover(persons[i], score));
            }
            return persons;
        }
    }
}
