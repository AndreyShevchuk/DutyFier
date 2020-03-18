using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DutyFier.Core.Models
{
    public class SettingsModel
    {
        private IRepository<Person> PersonRepository { get; set; }
        private IRepository<Position> PositionRepository { get; set; }
        private IRepository<DaysOfWeekWeight> DaysOfWeekWeightRepository { get; set; }

        public SettingsModel(IRepository<Person> personRepository, IRepository<Position> positionRepository, IRepository<DaysOfWeekWeight> daysOfWeekWeightRepository)
        {
            this.PersonRepository = personRepository;
            this.PositionRepository = positionRepository;
            this.DaysOfWeekWeightRepository = daysOfWeekWeightRepository;
        }
        
        public void RemovePosition(Position position) => PositionRepository.Delete(position);

        public void RemovePerson(Person person) => PersonRepository.Delete(person);

        public void UpdatePersonDependencyToPosition(Person selectedPerson)
        {
            PersonRepository.Update(selectedPerson);
        }

        public void UpdatePositionDependencyToPerson(List<Position> positions)
        {
            positions.ForEach(a => PositionRepository.Update(a));
        }

        public List<DaysOfWeekWeight> GetAllDaysOfWeek() => DaysOfWeekWeightRepository.GetAll().ToList(); 
        public List<Position> GetAllPosition() => PositionRepository.GetAll().ToList();
        public List<Person> GetAllPerson() => PersonRepository.GetAll().ToList();

        public void CahngeWeightDays(DaysOfWeekWeight daysOfWeekWeight)
        {
            DaysOfWeekWeightRepository.Update(daysOfWeekWeight);
        }
    }
}