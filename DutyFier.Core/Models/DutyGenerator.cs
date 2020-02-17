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
            if (requests.Count == 0)
            {
                return new List<Duty>();
            }

            var persons = this._personsRepository.GetAll().ToList();
            var feedbacks = this._feedbackRepository.GetAll().ToList();
            var generetedDuty = this._dutyRepository.GetAll().ToList();
            var daysOfWeekWeights = this._daysOfWeekWeights.GetAll().ToList();

            var coverPerson = PersonScoreCover.GetPersonScoreCoverList(persons, feedbacks, generetedDuty);
            

            return requests
                .Select(request => GenerateSingleDuty(coverPerson, daysOfWeekWeights, request, excludes, changeOnDateWeigths))
                .ToList()
            ;
        }

        private Duty GenerateSingleDuty(List<PersonScoreCover> persons, List<DaysOfWeekWeight> daysOfWeekWeights, DutyRequest request, List<ExcludeDates> excludes, List<ChangeOnDateWeigth> changeOnDateWeigths)
        {
            Duty duty = new Duty() { Date = request.Date };
            // for each position:
            PersonScoreCover person;
            for (int i = 0; i < request.Positions.Count; i++)
            {
                // Get best person who can be in this duty
                person = GetBestCandidat(persons, request, excludes, request.Positions[i]);

                //Person can`t be in two or more duty on a single day and it can`t be in a duty two days incommon
                AddExludeDatesForPersonInDuty(excludes, person, request);

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
            return duty;
        }

        private PersonScoreCover GetBestCandidat(List<PersonScoreCover> persons, DutyRequest request, List<ExcludeDates> excludes, Position position)
        {
            return persons.Where(a => a.Positions.Contains(position)).
                // Sort that persons
                OrderBy(a => a.Score).
                // Exclude person hwo has exclude in this day
                Where(a => !excludes.
                        Where(b => b.DateTimes.
                            Contains(request.Date)).
                        Select(b => b.Person).
                        Contains(a)).
                First()
            ;
        }

        private void AddExludeDatesForPersonInDuty(List<ExcludeDates> excludes , PersonScoreCover person, DutyRequest request)
        {
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
        }
    }
}
