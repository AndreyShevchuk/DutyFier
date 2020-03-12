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

        private DutyTypeRepository dutyTypeRepository;

        public PersonRepository personRepository { get; set; }
        public DutyRepository DutyRepository { get; set; } 
        public Dictionary<DutyType, List<DateTime>> DutyTypeDate { get; set; }
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
            dutyTypeRepository = new DutyTypeRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            personRepository = new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            this.DutyRepository = new DutyRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            dutyGenerate = new DutyGenerator(personRepository,
                                             new PersonDutyFeedbackRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()), 
                                             DutyRepository, 
                                             new DaysOfWeekWeightRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
            ExludeDates = personRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
            var dtypes = dutyTypeRepository.GetAll().ToList();
            DutyTypeDate = dtypes.ToDictionary(x => x, x => new List<DateTime>());
        }
        public void GeneratorRun()
        {
            var dutyRepository =  new DutyRepository( MainWindowViewModel.Container.Resolve<DutyFierContext>());
            duties = new ObservableCollection<Duty>(dutyGenerate.Generate(GetUnitedDutyRequsets(dutyRequests.ToList()), ConvertToListExludeDates(), new List<ChangeOnDateWeigth>().ToList()));
            
            dutyRepository.AddRange(duties);
        }

        private List<DutyRequest> GetUnitedDutyRequsets(List<DutyRequest> dutyRequests)
        {
            List<DutyRequest> res = new List<DutyRequest>();
            while (dutyRequests.Count>0)
            {
                res.Add(GetDutyUnitedRequest(ref dutyRequests, dutyRequests.First().DutyType, dutyRequests.First().Date));
            }
            
            return res;
        }

        private DutyRequest GetDutyUnitedRequest(ref List<DutyRequest> dutyRequests, DutyType dutyType, DateTime date)
        {
            var bar = dutyRequests;
            var requests = dutyRequests.Where(request => request.Date.Equals(date) && request.DutyType.Equals(dutyType)).ToList();
            var positions = dutyRequests.SelectMany(request => request.Positions).ToList();
            requests.ForEach(request => bar.Remove(request));
            return new DutyRequest() { Date = date, DutyType = dutyType, DutyTypeId = dutyType.Id, Positions = positions };
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
            DutyRequest dutyRequest;
            List<Position> positionsDublicateList;
            for (int i = 0; i < DutyTypeDate.Keys.Count; i++)
            {
                for (int j = 0; j < DutyTypeDate[DutyTypeDate.Keys.ElementAt(i)].Count; j++)
                {
                    for (int k = 0; k < DutyTypeDate.Keys.ElementAt(i).Positions.Count; k++)
                    {
                        positionsDublicateList = new List<Position>();
                        for (int l = 0; l < DutyTypeDate.Keys.ElementAt(i).Positions[k].DefaultPositionCount; l++)
                        {
                            positionsDublicateList.Add(DutyTypeDate.Keys.ElementAt(i).Positions[k]);
                        }

                        dutyRequest = new DutyRequest
                        {
                            Date = DutyTypeDate[DutyTypeDate.Keys.ElementAt(i)][j],
                            Positions = positionsDublicateList,
                            DutyType = DutyTypeDate.Keys.ElementAt(i),
                            DutyTypeId = DutyTypeDate.Keys.ElementAt(i).Id
                        };
                        dutyRequestsOnTheCalendar.Add(dutyRequest);
                    }
                }
            }
            return dutyRequestsOnTheCalendar;
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
        }
    }
}
