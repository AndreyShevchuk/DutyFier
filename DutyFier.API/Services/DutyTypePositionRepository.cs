using DutyFier.API.DbContexts;
using DutyFier.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyFier.API.Services
{
    public class DutyTypePositionRepository
    {
        private readonly DutyFierContext _context;
        public DutyTypePositionRepository(DutyFierContext context)
        {
            _context = context;
        }

        public IEnumerable<DutyType> GetDutyType()
        {
            return _context.DutyTypes;
        }

        public DutyType GetDutyType(int id)
        {
            return _context.DutyTypes.FirstOrDefault(e => e.Id == id);
        }
        
        public void AddDutyType(DutyType dutyType)
        {
            _context.Add(dutyType);
        }
        public void DeliteDutyType(DutyType dutyType)
        {
            _context.DutyTypes.Remove(dutyType);
        }

        public IEnumerable<Position> GetPositions()
        {
            return _context.Positions
                .Include(e => e.Executors)
                .Include(e => e.PositionPersons);
        }

        public Position GetPosition(int id)
        {
            return _context.Positions.FirstOrDefault(e => e.Id == id);
        }

        public void DelitePosition(Position position)
        {
            _context.Positions.Remove(position);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
