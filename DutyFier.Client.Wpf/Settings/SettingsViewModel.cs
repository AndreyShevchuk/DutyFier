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
        List<Person> people = new List<Person>();
        Dictionary<string, List<Position>> keyValuePairs = new Dictionary<string, List<Position>>();
        List<string> names = new List<string>();
        List<Position> positions = new List<Position>();
        public void Fuller()
        {
            for (int i = 0; i < people.Count; i++)
            {
                keyValuePairs.Add(people[i].FirstName + " " + people[i].LastName, people[i].Positions.ToList());
                names.Add(people[i].FirstName + " " + people[i].LastName);
                positions = (people[i].Positions).ToList();
            }
        }

        
    }
}

