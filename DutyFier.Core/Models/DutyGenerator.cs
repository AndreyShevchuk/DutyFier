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

            if(requests==null || requests.Count <= 0)
                throw new ArgumentException("Null or empty request parametr");
            return requests
                .Select(request => GenerateSingleDuty(ref coverPerson, daysOfWeekWeights, request, ref excludes, changeOnDateWeigths, persons))
                .ToList()
            ;
        }

        private Duty GenerateSingleDuty(ref List<PersonScoreCover> personCover, List<DaysOfWeekWeight> daysOfWeekWeights, DutyRequest request, ref List<ExcludeDates> excludes, List<ChangeOnDateWeigth> changeOnDateWeigths, List<Person> persons)
        {
            Duty duty = new Duty() { Date = request.Date };

            // for each position:
            PersonScoreCover person;
            if (request.Positions == null || request.Positions.Count <= 0)
                throw new ArgumentException("Null or empty request position parametr");
            for (int i = 0; i < request.Positions.Count; i++)
            {
                // Get best person who can be in this duty
                person = GetBestCandidate(personCover, request, excludes, request.Positions[i]);
                personCover.Contains(person);
                //Person can`t be in two or more duty on a single day and it can`t be in a duty two days incommon
                AddExludeDatesForPersonInDuty(ref excludes, person, request.Date);

                // Adding new executer with this person
                persons.Where(pers => pers.Id == person.Person.Id).First();
                duty.Executors.Add(new Executor() { Person = person.Person, Position = request.Positions[i]});
                person.Score += GetScoreCount(
                    person.Person.Factor,
                    request.Positions[i].Weight,
                    changeOnDateWeigths.Count > 0 && changeOnDateWeigths.Select(a => a.ChangedDateTime).Contains(request.Date) ?
                        changeOnDateWeigths.Find(a => a.ChangedDateTime.DayOfWeek.Equals(request.Date.DayOfWeek)).NewWeigth:
                        daysOfWeekWeights.Find(a => a.Day.Equals(request.Date.DayOfWeek)).Weight
                    );
                duty.PreliminaryAssessmentList.Add(person.Score);
            }
            return duty;
        }

        private double GetScoreCount(double factor, double positionWeight, double daysOfWeekWeight) => factor * (daysOfWeekWeight + positionWeight);

        private PersonScoreCover GetBestCandidate(List<PersonScoreCover> persons, DutyRequest request, List<ExcludeDates> excludes, Position position)
        {
            var tempPersonSCAvailableCollection = persons.Where(person =>
            {
                if (person.Person.Positions == null || person.Person.Positions.Count == 0)
                {
                    //throw new ArgumentException("Null or empty person available position parametr");
                    return false;
                }
                return person.Person.Positions.Contains(position);
            });
            if(tempPersonSCAvailableCollection.Count()==0)
                throw new ArgumentException("Can`t find available person to this request");
            if(excludes.Count==0)
                return tempPersonSCAvailableCollection.OrderBy(a => a.Score).First();
            IEnumerable<ExcludeDates> excludeDates;

            // Exclude person hwo has exclude in this day
            tempPersonSCAvailableCollection = tempPersonSCAvailableCollection.Where(personSC =>
            {
                excludeDates = excludes.
                        Where(b => b.DateTimes.
                            Contains(request.Date));
                if (excludeDates.Count() == 0)
                    return true;
                return !excludeDates.Select(b => b.Person).
                    Contains(personSC.Person);
                        
            });
            if (tempPersonSCAvailableCollection.Count() == 0)
                throw new ArgumentException("Can`t find available person to this request");

            return tempPersonSCAvailableCollection.
                // Sort that persons
                OrderBy(a => a.Score).
                First()
            ;
        }

        private void AddExludeDatesForPersonInDuty(ref List<ExcludeDates> excludes , PersonScoreCover person, DateTime requestDate)
        {
            var excludeDatesWithChosenPerson = excludes.Where(a => a.Person.Equals(person));
            if (excludeDatesWithChosenPerson.Count() == 0)
            {
                excludes.Add(new ExcludeDates(person.Person, new HashSet<DateTime> { requestDate, requestDate.AddDays(1) }));
            }
            else
            {
                excludeDatesWithChosenPerson.First().DateTimes.Add(requestDate);
                excludeDatesWithChosenPerson.First().DateTimes.Add(requestDate.AddDays(1));
            }
        }
    }
}
