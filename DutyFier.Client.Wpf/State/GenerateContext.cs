using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DutyFier.Client.Wpf.State
{
    public class GenerateContext
    {
        private DutyGenerator dutyGenerate;
        public PositionRepository positionRepository { get; set; }
        public PersonRepository personRepository { get; set; }
        public DutyRepository DutyRepository { get; set; } 
        public Dictionary<Position, List<DateTime>> PositionsDate { get; set; }
        public Dictionary<Person, List<DateTime>> ExludeDates { get; set; }

        public ObservableCollection<Duty> duties;

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
        private List<ExcludeDates> ConvertToListExludeDates()
        {
            var resault = new List<ExcludeDates>();
            foreach (var position in ExludeDates.Keys)
            {
                resault.Add(new ExcludeDates(position, ExludeDates[position]));
            }
            return resault;
        }


        public GenerateContext()
        {
            positionRepository = new PositionRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            personRepository = new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            this.DutyRepository = new DutyRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            dutyGenerate = new DutyGenerator(personRepository,
                                             new PersonDutyFeedbackRepository(), 
                                             DutyRepository, 
                                             new DaysOfWeekWeightRepository());
            ExludeDates = personRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
            PositionsDate = positionRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
        }
        public void GeneratorRun()
        {
            var a = MainWindowViewModel.Container.Resolve<DutyFierContext>();
            a.Duties.RemoveRange(a.Duties);
            a.SaveChanges();

            duties = new ObservableCollection<Duty>(dutyGenerate.Generate(dutyRequests.ToList(), ConvertToListExludeDates(), new List<ChangeOnDateWeigth>().ToList()));
            
            a.Duties.AddRange(duties);
            a.SaveChanges();
        }

        public void GeneratorRunWhereNoDutys()
        {
            if (duties == null)
            {
                GeneratorRun();
            }
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

        public void Reload()
        {
            duties.Clear();
            foreach (var item in DutyRepository.GetAll())
            {
                duties.Add(item);
            }
        }

        public void Update()
        {
            //r exRep = new ExecuterRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            foreach (var duty  in duties)
            {
                DutyRepository.Update(duty);

            }
            //TODO here could be OnPropCH
        }
    }
}
