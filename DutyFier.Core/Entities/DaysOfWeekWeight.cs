using System;

namespace DutyFier.Core.Entities
{
    public class DaysOfWeekWeight
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public double Weight { get; set; }
    }
}
