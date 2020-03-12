using System;
using System.Collections.Generic;
using System.Text;

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
        public string PositionNames
        {
            get
            {
                StringBuilder names = new StringBuilder();
                Executors.ForEach(ex => names.Append(ex.Position.Name + "\n\r"));
                return names.ToString();
            }
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
        public string ExecutorsPositions
        {
            get
            {
                string executorsPosition = "";
                foreach (var item in Executors)
                {
                    executorsPosition += $"{item.Position.Name} \n\r";
                }
                return executorsPosition;
            }
            private set { }
        }
        public string PreliminaryAssessments
        {
            get
            {
                string preliminaryAssessments = "";
                foreach (var item in Executors)
                {
                    preliminaryAssessments += $"{item.PreliminaryScore} \n\r";
                }
                return preliminaryAssessments;
            }
            private set { }
        }
    }
}
