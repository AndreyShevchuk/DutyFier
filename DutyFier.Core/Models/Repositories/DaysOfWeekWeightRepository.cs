using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyFier.Core.Repository
{
    public class DaysOfWeekWeightRepository : IRepository<DaysOfWeekWeight>
    {
        DutyFierContext context;

        public DaysOfWeekWeightRepository()
        {
            context = new DutyFierContext();
        }

        public void AddRange(ICollection<DaysOfWeekWeight> values)
        {
            context.DaysOfWeekWeights.AddRange(values);
            context.SaveChanges();
        }

        public void Create(DaysOfWeekWeight value)
        {
            context.DaysOfWeekWeights.Add(value);
            context.SaveChanges();
        }

        public void Delete(DaysOfWeekWeight value)
        {
            context.Entry(value).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public ICollection<DaysOfWeekWeight> GetAll()
        {
            return context.DaysOfWeekWeights.ToList();
        }

        public DaysOfWeekWeight GetOne(int id)
        {
            return
            context.DaysOfWeekWeights
            .First(e => e.Id == id);
        }

        public void Update(DaysOfWeekWeight value)
        {
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
        }


    }
}
