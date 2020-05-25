using AutoMapper;
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
    [Route("api/dutytypes{ditytypeId}/position")]
    public class PositionController : ControllerBase
    {
        IMapper _mapper;
        PositionRepository _positionRepository;
        public PositionController(IMapper mapper, PositionRepository positionRepository)
        {
            _mapper = mapper;
            _positionRepository = positionRepository;
        }

        [HttpGet]
        public ActionResult<PositionDto> GetPosition(int ditytypeId)
        {
            var position = _positionRepository.GetPosition(ditytypeId);
            if(position == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PositionDto>(position));
        }

        [HttpPost("{positionId}")]
        public ActionResult<PositionDto> CreatePositionForDutyType(int ditytypeId , PositionForCreateDto positionForCreate)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Entities.Course>(course);
            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            _courseLibraryRepository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourseForAuthor",
                new { authorId = authorId, courseId = courseToReturn.Id },
                courseToReturn);
        }
    }
}
