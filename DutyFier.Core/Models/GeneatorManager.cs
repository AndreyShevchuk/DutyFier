using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DutyFier.Core.Repository;
using DutyFier.Core.Interfaces;
using System.Linq;

namespace DutyFier.Core.Models
{
    class GeneatorManager
    {
        private List<DutyRequest> dutyRequests;
        private List<ExcludeDates> excludeDates;
        
        public IRepository<Person> PersonRepository;
        public IRepository<Position> PositionRepository;

        public GeneatorManager(IRepository<Person> personRepo, IRepository<Position> positionRepo)
        {
            dutyRequests = new List<DutyRequest>();
            excludeDates = new List<ExcludeDates>();
            PersonRepository = personRepo;
            PositionRepository = positionRepo;
        }
        public void GetDutyRequest(Position position, List<DateTime> dateTimes)
        {
            //dutyRequests.Where( e => dateTimes.Contains(e.Date) && e.DutyType.Equals(position.DutyType)).
        }

        public void GetExcludeDates(Dictionary<Position, List<DateTime>> dictionary)
        {

        }

        public void ComandChangeExecutorsDuty (Duty duty)  // ResaultVievModel
        {

        } 

    }
}
