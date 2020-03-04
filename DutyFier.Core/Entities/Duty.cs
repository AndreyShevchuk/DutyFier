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
        public List<double> PreliminaryAssessmentList { get; set; }


        public Duty()
        {
            Executors = new List<Executor>();
            PreliminaryAssessmentList = new List<double>();
        }
        public Duty(Duty copyDuty)
        {
            this.Id = copyDuty.Id;
            this.Date = copyDuty.Date;
            this.Executors = new List<Executor>(copyDuty.Executors);
            this.IsApproved = copyDuty.IsApproved;
            this.PreliminaryAssessmentList = copyDuty.PreliminaryAssessmentList;
        }
        public string PositionNames
        {
            get
            {
                return Executors[0].Position.Name;
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
                foreach (var item in preliminaryAssessments)
                {
                    preliminaryAssessments += $"{item} \n\r";
                }
                return preliminaryAssessments;
            }
            private set { }
        }
    }
}
