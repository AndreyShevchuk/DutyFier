using System;
using System.Collections.Generic;

namespace DutyFier.Core.Entities
{
    public class Duty
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual List<Executor> Executors { get; set; }
        public bool IsApproved { get; set; }

        public Duty()
        {
            Executors = new List<Executor>();
        }

        public Duty(Duty copyDuty)
        {
            this.Id = copyDuty.Id;
            this.Date = copyDuty.Date;
            this.Executors = new List<Executor>(copyDuty.Executors);
            this.IsApproved = copyDuty.IsApproved;
        }

        public string ExecutorsNames
        {
            get 
            {
                string executorsName = "";
                foreach (var item in Executors)
                {
                    executorsName += $"{item.Person.FirstName} {item.Person.LastName} \n\r";
                }
                return executorsName;
            }
            private set { }
        }
    }
}
