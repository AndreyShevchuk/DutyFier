using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyFier.Core
{
    public class PositionRepository : IRepository<Position>
    {
        DutyFierContext context;
        public PositionRepository()
        {
            context = new DutyFierContext();
        }

        public PositionRepository(DutyFierContext context)
        {
            this.context = context;
        }

        public void AddRange(ICollection<Position> values)
        {
            context.Positions.AddRange(values);
            context.SaveChanges();
        }

        public void Create(Position value)
        {
            context.Positions.Add(value);
            context.SaveChanges();
        }

        public void Delete(Position value)
        {
            context.Entry(value).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public ICollection<Position> GetAll()
        {
            return
                context.Positions
                .Include(e => e.Persons)
                .Include(e => e.DutyType)
                .ToList();
        }

        public Position GetOne(int id)
        {
            return
            context.Positions
            .Include(e => e.Persons)
            .Include(e => e.DutyType)
            .First(e => e.Id == id);
        }

        public void Update(Position value)
        {
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
