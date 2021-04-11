using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Filters;
using UoW.Api.Domain.Interfaces;
using UoW.Api.DTOs;

namespace UoW.Api.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public StudentController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _uow.StudentRepository.GetAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<Student> GetById(Guid id)
        {
            return await _uow.StudentRepository.GetByIdAsync(id);
        }

        [HttpGet("full")]
        public async Task<IEnumerable<Student>> GetFull()
        {
            return await _uow.StudentRepository.GetFullAsync();
        }

        [HttpGet("full/{id:guid}")]
        public async Task<Student> GetFullById(Guid id)
        {
            return await _uow.StudentRepository.GetFullByIdAsync(id);
        }

        [HttpPost("filter")]
        public async Task<IEnumerable<Student>> Filter([FromBody] StudentFilter filter)
        {
            return await _uow.StudentRepository.FilterAsync(filter);
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
