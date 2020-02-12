using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyFier.Core.Repository
{
    public class DutyTypeRepository : IRepository<DutyType>
    {
        DutyFierContext context;

        public DutyTypeRepository()
        {
            context = new DutyFierContext();
        }

        public void AddRange(ICollection<DutyType> values)
        {
            context.DutyTypes.AddRange(values);
            context.SaveChanges();
        }

        public void Create(DutyType value)
        {
            context.DutyTypes.Add(value);
            context.SaveChanges();
        }

        public void Delete(DutyType value)
        {
            context.Entry(value).State = EntityState.Deleted;
        }

        public ICollection<DutyType> GetAll()
        {
            return context.DutyTypes.ToList();
        }

        public DutyType GetOne(int id)
        {
            return context.DutyTypes.First(a => a.Id == id);
        }

        public void Update(DutyType value)
        {
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
