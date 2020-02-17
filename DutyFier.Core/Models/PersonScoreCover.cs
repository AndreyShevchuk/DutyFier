using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace DutyFier.Core.Models
{
    class PersonScoreCover : Person
    {
        public double Score { get; set; }

        public PersonScoreCover()
        {

        }

        public PersonScoreCover(Person person, double score) : base(person) => Score = score;

        public static List<PersonScoreCover> GetPersonScoreCoverList(List<Person> persons, List<PersonDutyFeedback> feedbacks, List<Duty> generetedDuty)
        {
            List<PersonScoreCover> personScoreCovers = new List<PersonScoreCover>();
            double score;
            for (int i = 0; i < persons.Count; i++)
            {
                score = 
                    feedbacks.Where(a => a.Person.Equals(persons[i])).Sum(a => a.Source) + 
                    generetedDuty.Where(a => !a.IsApproved).SelectMany(a => a.Executors).Where(a => a.Person.Equals(persons[i])).Sum(a => a.Score)
                ;
                personScoreCovers.Add(new PersonScoreCover(persons[i], score));
            }
            return personScoreCovers;
        }
    }
}
