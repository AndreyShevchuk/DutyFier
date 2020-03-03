using System;
using System.Collections.Generic;

namespace DutyFier.Core.Entities
{
    public class ExcludeDates
    {
        public Person Person { get; private set; }

        public HashSet<DateTime> DateTimes { get; private set; }

        public ExcludeDates(Person person, IEnumerable<DateTime> dateTimes)
        {
            Person = person;
            DateTimes = new HashSet<DateTime>(dateTimes);
        }

        public override bool Equals(object obj)
        {
            return obj is ExcludeDates dates &&
                   EqualityComparer<Person>.Default.Equals(Person, dates.Person) &&
                   EqualityComparer<HashSet<DateTime>>.Default.Equals(DateTimes, dates.DateTimes);
        }

        public override int GetHashCode()
        {
            var hashCode = -715399687;
            hashCode = hashCode * -1521134295 + EqualityComparer<Person>.Default.GetHashCode(Person);
            hashCode = hashCode * -1521134295 + EqualityComparer<HashSet<DateTime>>.Default.GetHashCode(DateTimes);
            return hashCode;
        }
    }
}
