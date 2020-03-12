using System.Collections.Generic;

namespace DutyFier.Core.Entities
{
    public class DutyType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Position> Positions { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
        //  public virtual List<Duty> DutyTypes { get; set; }
    }
}
