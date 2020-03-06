﻿namespace DutyFier.Core.Entities
{
    public class PersonDutyFeedback
    {
        public int Id { get; set; }
        public int DutyId { get; set; }
        public virtual Duty Duty { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        //TODO Change name Source to Score
        public double Score { get; set; }
        //TODO find if it use
    }
}
