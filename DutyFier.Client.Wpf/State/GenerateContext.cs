using DutyFier.Core.Entities;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
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


        public GenerateContext()
        {
            positionRepository = new PositionRepository();
            personRepository = new PersonRepository();

            PositionsDate = positionRepository.GetAll().ToDictionary(x => x, x => new List<DateTime>());
        }

    }
}
