using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Models
{
    public class AdTypeModel
    {
        private IRepository<DutyType> DutyTypeRepository { get; set;}

        public AdTypeModel(IRepository<DutyType> dutyTypeRepository)
        {
            DutyTypeRepository = dutyTypeRepository;
        }

        public void AddDutyTypeToDB(DutyType dutyType)
        {
            DutyTypeRepository.Create(dutyType);
        }
    }
}
