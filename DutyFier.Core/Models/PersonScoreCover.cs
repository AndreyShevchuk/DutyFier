using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace DutyFier.Core.Models
{
    public class PersonScoreCover 
    {
        public Person Person { get; set; }
        public double Score { get; set; }

        public PersonScoreCover(Person person, double score) 
        {
            Person = person;
            Score = score;
        }

        public PersonScoreCover() { }
        public static List<PersonScoreCover> GetPersonScoreCoverList(List<Person> persons, List<PersonDutyFeedback> feedbacks, List<Duty> generetedDuty)
        {
            List<PersonScoreCover> personScoreCovers = new List<PersonScoreCover>();
            double score;
            for (int i = 0; i < persons.Count; i++)
            {
                score =
                    feedbacks.Where(a => a.Person.Equals(persons[i])).Sum(a => a.Source) +
                    generetedDuty.
                    //Select only duties where persons[i] contain
                        Where(duty => duty.Executors.Select(a => a.Person).Contains(persons[i])).
                        //Select only duties what is not approved
                        Where(duty => !duty.IsApproved).
                        //Getting all double values of each person
                        Select(duty => 
                            duty.PreliminaryAssessmentList[
                                duty.Executors.
                                    Select(executor => executor.Person).
                                    ToList().
                                    IndexOf(persons[i])
                                ]).
                        Sum()
                ;
                personScoreCovers.Add(new PersonScoreCover(persons[i], score));
            }
            return personScoreCovers;
        }
        public static List<PersonScoreCover> GetPersonScoreCoverList(List<Person> persons, List<PersonDutyFeedback> feedbacks)
        {
            List<PersonScoreCover> personScoreCovers = new List<PersonScoreCover>();
            double score;
            for (int i = 0; i < persons.Count; i++)
            {
                score =
                    feedbacks.Where(a => a.Person.Equals(persons[i])).Sum(a => a.Source)
                    ;
                personScoreCovers.Add(new PersonScoreCover(persons[i], score));
            }
            return personScoreCovers;
        }
    }
}
