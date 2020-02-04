using DutyFier.Core.Entities;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Statistics
{
    class StatisticsViewModel
    {
        public List<Person> Persons { get; set; }
        public List<PersonDutyFeedback> Feedbacks { get; set; }

        public StatisticsViewModel()
        {
            Persons = new PersonRepository().GetAll().ToList();
            Feedbacks = new PersonDutyFeedbackRepository().GetAll().ToList();
            //InizializeScore();
        }

        //moved from old project

        //private void InizializeScore()
        //{
        //    for (int i = 0; i < Persons.Count; i++)
        //        Persons[i].Score = Feedbacks.Where(a => a.Person.Equals(Persons[i])).Select(a => a.Scrore).Sum();
        //}
    }
}
