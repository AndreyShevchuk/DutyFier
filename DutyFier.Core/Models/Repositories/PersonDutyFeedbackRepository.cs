using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace DutyFier.Core.Repository
{
    public class PersonDutyFeedbackRepository : IRepository<PersonDutyFeedback>
    {
        private DutyFierContext context;
        public PersonDutyFeedbackRepository()
        {
            context = new DutyFierContext();
        }

        public void AddRange(ICollection<PersonDutyFeedback> values)
        {
            context.PersonDutyFeedbacks.AddRange(values);
            context.SaveChanges();
        }

        public void Create(PersonDutyFeedback value)
        {
            context.PersonDutyFeedbacks.Add(value);
            context.SaveChanges();
        }

        public void Delete(PersonDutyFeedback value)
        {
            context.Entry(value).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public ICollection<PersonDutyFeedback> GetAll()
        {
            return context.PersonDutyFeedbacks.ToList();
        }

        public PersonDutyFeedback GetOne(int id)
        {
            return
            context.PersonDutyFeedbacks
            .Include(e => e.Person)
            .Include(e => e.Duty)
            .First(e => e.Id == id);
        }

        public void Update(PersonDutyFeedback value)
        {
            throw new NotImplementedException();
        }
    }
}
