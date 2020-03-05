using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyFier.Core
{
    public class ExecuterRepository : IRepository<Executor>
    {
        DutyFierContext context;
        public ExecuterRepository()
        {
            context = new DutyFierContext();
        }

        public ExecuterRepository(DutyFierContext context)
        {
            this.context = context;
        }

        public void AddRange(ICollection<Executor> values)
        {
            context.Executors.AddRange(values);
            context.SaveChanges();
        }

        public void Create(Executor value)
        {
            context.Executors.Add(value);
            context.SaveChanges();
        }

        public void Delete(Executor value)
        {
            context.Entry(value).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public ICollection<Executor> GetAll()
        {
            return context.Executors.ToList();
        }

        public Executor GetOne(int id)
        {
            return
             context.Executors
             .Include(e => e.Person)
             .Include(e => e.Duty)
             .First(e => e.Id == id);
        }

        public void Update(Executor value)
        {
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
