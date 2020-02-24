using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DutyFier.Core.Models
{
    public class DutyModel
    {
        private int DutyID { get; set; }
        public DateTime Date { get; set; }
        public List<Position> Positions { get; set; }
        public List<Person> Persons { get; set; }
        public List<double> Scrores { get; set; }

        public DutyModel(int dutyID, DateTime date, List<Position> positions, List<Person> persons, List<double> scrores)
        {
            DutyID = dutyID;
            Date = date;
            Positions = positions;
            Persons = persons;
            Scrores = scrores;
        }

        public static DutyModel[] GetDutiesWitchHasNoFeedbacks(Interfaces.IRepository<Duty> repository)
        {
            var dutyModelList = new List<DutyModel>();
            var dutysList = repository.GetAll().Where(a => !a.IsApproved).ToList();
            for (int i = 0; i < dutysList.Count; i++)
            {
                dutyModelList.Add(
                    new DutyModel(
                        dutysList[i].Id, 
                        dutysList[i].Date, 
                        dutysList[i].Executors.Select(a => a.Position).ToList(),
                        dutysList[i].Executors.Select(a => a.Person).ToList(), 
                        dutysList[i].Executors.Select(a => a.Score).ToList())
                    );
            }
            return dutyModelList.ToArray();
        }


    }
}
