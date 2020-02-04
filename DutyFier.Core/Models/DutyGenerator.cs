using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutyFier.Core.Models
{
    public class DutyGenerator
    {
        private IRepository<Person> _personsRepository { get; }
        private IRepository<PersonDutyFeedback> _feedbackRepository { get; }
        private IRepository<Duty> _dutyRepository { get; }
        private IRepository<DaysOfWeekWeight> _daysOfWeekWeights { get; }

        public DutyGenerator(
            IRepository<Person> personsRepository,
            IRepository<PersonDutyFeedback> feedbackRepository,
            IRepository<Duty> dutyRepository,
            IRepository<DaysOfWeekWeight> daysOfWeekWeights
        )
        {
            this._personsRepository = personsRepository;
            this._feedbackRepository = feedbackRepository;
            this._dutyRepository = dutyRepository;
            this._daysOfWeekWeights = daysOfWeekWeights;
        }

        public IEnumerable<Duty> Generate(List<DutyRequest> requests, List<ExcludeDates> excludes, List<ChangeOnDateWeigth> changeOnDateWeigths)
        {
            var persons = this._personsRepository.
                GetAll().
                ToList()
                ;
            var feedbacks = this._feedbackRepository.
                GetAll().
                ToList()
                ;
            var generetedDuty = this._dutyRepository.
                GetAll().
                ToList()
                ;
            var daysOfWeekWeights = this._daysOfWeekWeights.
                GetAll().
                ToList()
                ;

            List<Duty> duties = new List<Duty>();
            // load all persons from database
            // calculate score for each person
            for (int i = 0; i < persons.Count; i++)
            {
                persons[i].Score = feedbacks.
                   Where(a => a.Person.Equals(persons[i])).
                   Sum(a => a.Source) + generetedDuty.
                   Where(a => !a.IsApproved).
                   SelectMany(a => a.Executors).
                   Where(a => a.Person.Equals(persons[i])).
                   Sum(a => a.Score)
                ;
            }

            // for each duty request -- create next duty from this duty request
            for (int i = 0; i < requests.Count; i++)
            {
                duties.Add(GenerateSingleDuty(persons, daysOfWeekWeights, requests[i], excludes, changeOnDateWeigths));
            }
            return duties;
        }

        private Duty GenerateSingleDuty(List<Person> persons, List<DaysOfWeekWeight> daysOfWeekWeights, DutyRequest request, List<ExcludeDates> excludes, List<ChangeOnDateWeigth> changeOnDateWeigths)
        {
            Duty duty = new Duty() { Date = request.Date };
            // for each position:
            Person person;
            for (int i = 0; i < request.Positions.Count; i++)
            {
                // Get all person who cound be in this duty
                person = persons.Where(a => a.Positions.Contains(request.Positions[i]))
                // Sort that persons
                    .OrderBy(a => a.Score)
                // Exclude person hwo has exclude in this day
                    .Where(a => !excludes.
                            Where(b => b.DateTimes.
                                Contains(request.Date)).
                            Select(b => b.Person).
                            Contains(a)).
                    First()
                    ;

                //Person can`t be in two or more duty on a single day and it can`t be in a duty two days incommon
                var excludeDatesWithChosenPerson = excludes.Where(a => a.Person.Equals(person));
                if (excludeDatesWithChosenPerson.Count() == 0)
                {
                    excludes.Add(new ExcludeDates(person, new HashSet<DateTime> { request.Date, request.Date.AddDays(1) }));
                }
                else
                {
                    excludeDatesWithChosenPerson.First().DateTimes.Add(request.Date);
                    excludeDatesWithChosenPerson.First().DateTimes.Add(request.Date.AddDays(1));
                }
                // Adding new executer with this person
                duty.Executors.Add(new Executor() { Person = person, Position = request.Positions[i] });
                //Adding new score to person
                if (changeOnDateWeigths.Select(a => a.ChangedDateTime).Contains(request.Date))
                {
                    person.Score += person.Factor * (request.AdditionalWeight + changeOnDateWeigths.Find(a => a.ChangedDateTime.DayOfWeek.Equals(request.Date.DayOfWeek)).NewWeigth);
                }
                else
                {
                    person.Score += person.Factor * (request.AdditionalWeight + daysOfWeekWeights.Find(a => a.Day.Equals(request.Date.DayOfWeek)).Weight);
                }
                //MessageBox.Show(person.Score + "");
            }
            // ret
            return duty;
        }
    }
}
