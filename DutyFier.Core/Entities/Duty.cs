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
    }
}
