using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyFier.Core.Repository
{
    public class PersonRepository : IRepository<Person>
    {
        DutyFierContext context;
        public PersonRepository()
        {
            context = new DutyFierContext();
        }

        public PersonRepository(DutyFierContext context)
        {
            this.context = context;
        }

        public void AddRange(ICollection<Person> values)
        {
            context.Persons.AddRange(values);
            context.SaveChanges();
        }

        public void Create(Person value)
        {
            context.Persons.Add(value);
            context.SaveChanges();
        }

        public void Delete(Person value)
        {
            context.Entry(value).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public ICollection<Person> GetAll()
        {
           return context.Persons.ToList<Person>();
            //return context.Persons
            //    .Include(e => e.PersonDutyFeedbacks)
            //    .Include(e => e.Positions)
            //    .ToList();
        }

        public Person GetOne(int id)
        {
            return
            context.Persons
            .Include(e => e.Positions)
            // .Include(e => e.PersonDutyFeedbacks)
            .First(e => e.Id == id);
        }

        public void Update(Person value)
        {
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Update(ICollection<Person> values)
        {
            foreach (var item in values)
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
