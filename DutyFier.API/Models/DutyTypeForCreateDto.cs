using DutyFier.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyFier.API.Models
{
    public class DutyTypeForCreateDto
    {
        public string Name { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
    }
}
