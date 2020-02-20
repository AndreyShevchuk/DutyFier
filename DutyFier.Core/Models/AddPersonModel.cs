using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Models
{
    public class AddPersonModel
    {
        private IRepository<Person> PersonRepository { get; set; }

        public AddPersonModel(IRepository<Person> personRepository)
        {
            PersonRepository = personRepository;
        }

        public void AddPersonToDB(Person person)
        {
            PersonRepository.Create(person);
        }
    }
}
