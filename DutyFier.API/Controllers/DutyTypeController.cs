using AutoMapper;
using DutyFier.API.Entities;
using DutyFier.API.Models;
using DutyFier.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyFier.API.Controllers
{
    [ApiController]
    [Route("api/dutytype")]
    public class DutyTypeController : ControllerBase
    {
        private readonly DutyTypePositionRepository _dutyTypePositionRepository;
        private readonly IMapper _mapper;
        public DutyTypeController(IMapper mapper, DutyTypePositionRepository dutyTypePositionRepository)
        {
            _mapper = mapper;
            _dutyTypePositionRepository = dutyTypePositionRepository;
        }

        [HttpGet]
        public ActionResult<ICollection<DutyType>> GetDutyType()
        {
            var dutytypes = _dutyTypePositionRepository.GetType();
            if(dutytypes == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<DutyTypeDto>>(dutytypes));
        }

        [HttpGet("{idDutyType}", Name = "GetDutytype")]
        public ActionResult<DutyType> GetDutytype(int idDutyType)
        {
            var dytytype = _dutyTypePositionRepository.GetDutyType(idDutyType);
            if(dytytype == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DutyTypeDto>(dytytype));
        }

        [HttpPost]
        public ActionResult<DutyTypeDto> CreateDutytype(DutyTypeForCreateDto dutyType)
        {
            var dutyTypeEntity = _mapper.Map<DutyType>(dutyType);
            _dutyTypePositionRepository.AddDutyType(dutyTypeEntity);
            _dutyTypePositionRepository.Save();

            var authorToReturn = _mapper.Map<DutyTypeDto>(dutyTypeEntity);
            return CreatedAtRoute("GetDutytype",
                new { idperson = authorToReturn.Id },
                authorToReturn);
        }

        [HttpDelete("{idDutyType}")]
        public ActionResult DeleteDutyType(int idDutyType)
        {
            var dutyType = _dutyTypePositionRepository.GetDutyType(idDutyType); //(idPosition);
            if(dutyType == null)
            {
                return NotFound();
            }

            _dutyTypePositionRepository.DeliteDutyType(dutyType);
            _dutyTypePositionRepository.Save();

            return NoContent();
        } 

    }
}
