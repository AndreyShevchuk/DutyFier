using System.Collections.Generic;

namespace DutyFier.Core.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Factor { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<PersonDutyFeedback> PersonDutyFeedbacks { get; set; }

        public Person()
        {
            Positions = new HashSet<Position>();
            PersonDutyFeedbacks = new HashSet<PersonDutyFeedback>();
        }

        public Person(string firstName, string lastName, double factor) : base()
        {
            FirstName = firstName;
            LastName = lastName;
            Factor = factor;
        }
        public Person(Person person) : base()
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Factor = person.Factor;
            Positions = person.Positions;
            PersonDutyFeedbacks = person.PersonDutyFeedbacks;
        }

        public override bool Equals(object obj)
        {
            return obj is Person person && Id == person.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
