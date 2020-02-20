using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Models
{
    public class AddPositionModel
    {
        public AddPositionModel(IRepository<Position> positionRepository)
        {
            PositionRepository = positionRepository;
        }

        private IRepository<Position> PositionRepository { get; set;}
        public void AddPositionToDB(Position _position)
        {
            PositionRepository.Create(_position);
        }
    }
}
