using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;

namespace DutyFier.Core.Entities
{
    public class DutyRequest
    {
        public double AdditionalWeight { get; set; }
        public DateTime Date { get; set; }
        public List<Position> Positions { get; set; }
        public int DutyTypeId { get; set; }
        public DutyType DutyType { get; set; }


        public DutyRequest(double additionalWeight, DateTime date, List<Position> positions, DutyType dutyType)
        {
            AdditionalWeight = additionalWeight;
            Date = date;
            Positions = positions;
            DutyType = dutyType;
        }

        public override bool Equals(object obj)
        {
            return obj is DutyRequest request &&
                   Date == request.Date &&
                   EqualityComparer<List<Position>>.Default.Equals(Positions, request.Positions) &&
                   EqualityComparer<DutyType>.Default.Equals(DutyType, request.DutyType);
        }

        public override int GetHashCode()
        {
            var hashCode = 1203840533;
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Position>>.Default.GetHashCode(Positions);
            hashCode = hashCode * -1521134295 + EqualityComparer<DutyType>.Default.GetHashCode(DutyType);
            return hashCode;
        }
    }
}
