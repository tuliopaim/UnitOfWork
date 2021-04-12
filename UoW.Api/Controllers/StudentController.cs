using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Filters;
using UoW.Api.Domain.Interfaces;
using UoW.Api.DTOs.Input;
using UoW.Api.DTOs.Output;

namespace UoW.Api.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StudentController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDto>> Get()
        {
            var list = await _uow.StudentRepository.GetAsync();

            return list?.Select(_mapper.Map<StudentDto>);
        }

        [HttpGet("{id:guid}")]
        public async Task<StudentDto> GetById(Guid id)
        {
            var entity = await _uow.StudentRepository.GetByIdAsync(id);

            return _mapper.Map<StudentDto>(entity);
        }

        [HttpGet("full")]
        public async Task<IEnumerable<StudentFullDto>> GetFull()
        {
            var list = await _uow.StudentRepository.GetFullAsync();

            return list?.Select(_mapper.Map<StudentFullDto>);
        }

        [HttpGet("full/{id:guid}")]
        public async Task<StudentFullDto> GetFullById(Guid id)
        {
            var entity = await _uow.StudentRepository.GetFullByIdAsync(id);

            return _mapper.Map<StudentFullDto>(entity);
        }

        [HttpPost("filter")]
        public async Task<IEnumerable<StudentFullDto>> Filter([FromBody] StudentFilter filter)
        {
            var list = await _uow.StudentRepository.FilterAsync(filter);

            return list?.Select(_mapper.Map<StudentFullDto>);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Student(model.Name, model.BirthDate);

            _uow.StudentRepository.Add(entity);

            await _uow.CommitAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _uow.StudentRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Remove();

            _uow.StudentRepository.Update(entity);

            await _uow.CommitAsync();

            return Ok();
        }
    }
}
