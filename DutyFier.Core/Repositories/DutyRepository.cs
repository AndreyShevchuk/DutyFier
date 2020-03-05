using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyFier.Core
{
    public class DutyRepository : IRepository<Duty>
    {
        DutyFierContext context;
        public DutyRepository()
        {
            context = new DutyFierContext();
        }

        public DutyRepository(DutyFierContext context)
        {
            this.context = context;
        }

        public void AddRange(ICollection<Duty> values)
        {
            context.Duties.AddRange(values);
            context.SaveChanges();
        }

        public void Create(Duty value)
        {
            context.Entry(value).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Delete(Duty value)
        {
            context.Duties.Remove(value);
            context.SaveChanges();
        }

        public ICollection<Duty> GetAll()
        {
            return context
                .Duties
                .Include(e => e.Executors)
                .ToList();
        }

        public Duty GetOne(int id)
        {
            return
            context.Duties
            .Include(e => e.Executors)
            .First(e => e.Id == id);
        }

        public void Update(Duty value)
        {
            
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
