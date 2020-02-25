using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;

namespace DutyFier.Core.Entities
{
    public class DutyRequest
    {
        public DateTime Date { get; set; }
        public List<Position> Positions { get; set; }
        public int DutyTypeId { get; set; }
        public DutyType DutyType { get; set; }

        public DutyRequest()
        {
            Positions = new List<Position>();
            Date = new DateTime();
            DutyType = new DutyType();
        }

        public DutyRequest(DateTime date, List<Position> positions, DutyType dutyType)
        {
            Date = date;
            Positions = positions;
            DutyType = dutyType;
        }

        public override bool Equals(object obj)
        {
            return obj is DutyRequest request &&
                   Date == request.Date &&
                   DutyTypeId == request.DutyTypeId;
        }

        public override int GetHashCode()
        {
            var hashCode = 1414455357;
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + DutyTypeId.GetHashCode();
            return hashCode;
        }
    }
}
