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

        private ObservableCollection<DutyRequest> dutyRequests;
        public ObservableCollection<DutyRequest> DutyRequests {
            get
            {
                if(dutyRequests == null)
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
                            t.DutyTypeId = key.DutyTypeId;
                            dutyReqests.Add(t);
                        }
                    }
                    this.dutyRequests = dutyReqests;
                    return dutyReqests;
                }

                ChangeDutyReqest();
                return this.dutyRequests;
            } 
        } 

        public GenerateContext()
        {
            positionRepository = new PositionRepository();
            personRepository = new PersonRepository();

            ExludeDates = personRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
            PositionsDate = positionRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
        }



        private void ChangeDutyReqest()
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
                    t.DutyTypeId = key.DutyTypeId;
                    dutyReqests.Add(t);
                }
            }
            

            var newDuyReqest = dutyReqests.Except(this.dutyRequests, new DutyReqestComparer()).ToList(); /// new Element
            var oldDutyReqest = this.dutyRequests.Except(dutyReqests, new DutyReqestComparer()).ToList(); //

            foreach (var item in oldDutyReqest)
            {
                this.dutyRequests.Remove(item);
            }

            foreach (var item in newDuyReqest)
            {
                this.dutyRequests.Add(item);
            }

        }

        private void MinusDutyReqest()
        {

        }

        private void PlusDutyReqest()
        {
            
        }

    }
}
