using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DutyFier.Core.Models
{
    public class PersonModel
    {
        private IRepository<Person> PersonRepository { get; set; }
        public PersonModel(IRepository<Person> personRepository)
        {
            this.PersonRepository = personRepository;
        }
        public List<Person> GetAllPerson() => PersonRepository.GetAll().ToList();
        public void RemovePerson(Person person) => PersonRepository.Delete(person);
        public void UpdatePersonDependencyToPosition(Person selectedPerson)
        {
            PersonRepository.Update(selectedPerson);
        }
    }
}
