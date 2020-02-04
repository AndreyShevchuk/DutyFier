using System.Collections.Generic;

namespace DutyFier.Core.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Score { get; set; }
        public double Factor { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<PersonDutyFeedback> PersonDutyFeedbacks { get; set; }

        public Person()
        {
            Positions = new HashSet<Position>();
            PersonDutyFeedbacks = new HashSet<PersonDutyFeedback>();
        }


        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   Id == person.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
