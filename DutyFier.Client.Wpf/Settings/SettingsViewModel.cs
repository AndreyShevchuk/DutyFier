using DutyFier.Client.Wpf.Settings.Infrastructure;
using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Settings
{
    class SettingsViewModel
    {
        public readonly DelegateCommand _addOrRemoveGroupCommand;
        private DutyFierContext context;
        public PersonRepository personRepository { get; set; }
        public List<Person> Persons { get; set; }
        public List<Position> Positions { get; set; }

        public SettingsViewModel()
        {
            personRepository = new PersonRepository();
            _addOrRemoveGroupCommand = new DelegateCommand(AddOrRemoveGroup);
            context = new DutyFierContext();
            Persons = context.Persons.ToList();
            Positions = context.Positions.ToList();
        }

        public DelegateCommand AddOrRemoveGroupCommand
        {
            get { return _addOrRemoveGroupCommand; }
        }

        private void AddOrRemoveGroup(object parameter)
        {
            object[] parameters = parameter as object[];
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (!parameters.Length.Equals(3))
                throw new ArgumentException("Invalid number of arguments.", "parameters");
            if (!(parameters[0] is bool))
                throw new ArgumentException("First argument is invalid.", "parameters");

            Person person = parameters[1] as Person;
            if (person == null)
                throw new ArgumentException("Second argument is invalid.", "parameters");

            Position position = parameters[2] as Position;
            if (position == null)
                throw new ArgumentException("Third argument is invalid.", "parameters");

            bool isAdd = Convert.ToBoolean(parameters[0]);
            bool existsInCollecton = person.Positions.Contains(position);
            if (isAdd && !existsInCollecton)
                person.Positions.Add(position);
            else if (!isAdd && existsInCollecton)
                person.Positions.Remove(position);
        }

        public void SaveChange()
        {
            foreach (var item in Persons)
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}

