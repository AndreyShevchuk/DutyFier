using DutyFier.Core.Entities;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    public class GenerateContext
    {
        public PositionRepository positionRepository { get; set; }
        public PersonRepository personRepository { get; set; }
        public Dictionary<Position, List<DateTime>> PositionsDate { get; set; }
        public Dictionary<Person, List<DateTime>> ExludeDates { get; set; }
        public ObservableCollection<DutyRequest> DutyRequests { get
            {
                var dutyReqests = new ObservableCollection<DutyRequest>();
                foreach (var key in PositionsDate.Keys)
                {
                    foreach (var item in PositionsDate[key])
                    {
                        var t = new DutyRequest();
                        t.Date = item;
                        t.Positions.Add(key);
                        t.DutyType = key.DutyType;
                        dutyReqests.Add(t);
                    }
                }
                return dutyReqests;
            } 
        } 

        public GenerateContext()
        {
            positionRepository = new PositionRepository();
            personRepository = new PersonRepository();

            ExludeDates = personRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
            PositionsDate = positionRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
        }

    }
}
