using System;

namespace DutyFier.Core.Entities
{
    public class ChangeOnDateWeigth
    {
        public DateTime ChangedDateTime { get; private set; }
        public double NewWeigth { get; private set; }

        public ChangeOnDateWeigth(DateTime changedDateTime, double newWeigth)
        {
            ChangedDateTime = changedDateTime;
            NewWeigth = newWeigth;
        }

        public ChangeOnDateWeigth()
        {

        }
    }
}
