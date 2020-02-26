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

        public SettingsModel(IRepository<Person> personRepository, IRepository<Position> positionRepository)
        {
            this.PersonRepository = personRepository;
            this.PositionRepository = positionRepository;
        }

        public void RemovePerson(Person person) => PersonRepository.Delete(person);

        public void UpdatePersonDependencyToPosition(Person selectedPerson)
        {
            PersonRepository.Update(selectedPerson);
        }

        public void UpdatePositionDependencyToPerson(List<Position> positions)
        {
            positions.ForEach(a => PositionRepository.Update(a));
        }

        public List<Position> GetAllPosition() => PositionRepository.GetAll().ToList();
    }
}