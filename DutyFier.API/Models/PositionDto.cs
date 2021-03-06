﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyFier.API.Models
{
    public class PositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public bool IsSeniorPosition { get; set; }
        public int DefaultPositionCount { get; set; }
        public DutyTypeDto DutyType { get; set; }
    }
}
