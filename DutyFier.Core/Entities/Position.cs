using System.Collections.Generic;

namespace DutyFier.Core.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public virtual DutyType DutyType { get; set; }
        public int DutyTypeId { get; set; }
        public bool IsSeniorPosition { get; set; }
        public ICollection<Person> Persons { get; set; }
        public Position()
        {
            Persons = new List<Person>();
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Name == position.Name &&
                   Weight == position.Weight &&
                   DutyTypeId == position.DutyTypeId &&
                   IsSeniorPosition == position.IsSeniorPosition;
        }

        public override int GetHashCode()
        {
            var hashCode = -1172651899;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            hashCode = hashCode * -1521134295 + DutyTypeId.GetHashCode();
            hashCode = hashCode * -1521134295 + IsSeniorPosition.GetHashCode();
            return hashCode;
        }
    }
}
