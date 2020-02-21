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
                    dutyRequests = DutyRequestsOnTheCalendar();
                    return dutyRequests;
                }
                ChangeDutyReqest();
                return dutyRequests;
            } 
        } 

        public GenerateContext()
        {
            positionRepository = new PositionRepository();
            personRepository = new PersonRepository();

            ExludeDates = personRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
            PositionsDate = positionRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
        }



        private ObservableCollection<DutyRequest> DutyRequestsOnTheCalendar()
        {
            var dutyRequestsOnTheCalendar = new ObservableCollection<DutyRequest>();
            foreach (var SelectedPositionCalendar in PositionsDate.Keys)
            {
                foreach (var SelectedDatesforPosition in PositionsDate[SelectedPositionCalendar])
                {
                    var duteReqest = new DutyRequest();
                    duteReqest.Date = SelectedDatesforPosition;
                    duteReqest.Positions.Add(SelectedPositionCalendar);
                    duteReqest.DutyType = SelectedPositionCalendar.DutyType;
                    duteReqest.DutyTypeId = SelectedPositionCalendar.DutyTypeId;
                    dutyRequestsOnTheCalendar.Add(duteReqest);
                }
            }
            return  dutyRequestsOnTheCalendar;
        }

        private void ChangeDutyReqest()
        {
            var dutyReqests = DutyRequestsOnTheCalendar();
            RemoveOldDutyReqest(dutyReqests);
            SearchNewDutyReqest(dutyReqests);
        }
        private void RemoveOldDutyReqest(ICollection<DutyRequest> dutyRequestsOnTheCalendar)
        {
            var oldDutyReqestInCalendar = this.dutyRequests.ToList().Except(dutyRequestsOnTheCalendar.ToList(), new DutyReqestComparer()).ToList(); //
            foreach (var item in oldDutyReqestInCalendar)
            {
                this.dutyRequests.Remove(item);
            }
        }
        private void SearchNewDutyReqest(ICollection<DutyRequest> dutyRequestsOnTheCalendar)
        {
            var newDuyReqestInCalendar = dutyRequestsOnTheCalendar.ToList().Except(this.dutyRequests.ToList(), new DutyReqestComparer()).ToList();
            foreach (var item in newDuyReqestInCalendar)
            {
                this.dutyRequests.Add(item);
            }
        }

    }
}
