using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Filters;
using UoW.Api.Domain.Interfaces;
using UoW.Api.DTOs;

namespace UoW.Api.Controllers
{
    [ApiController]
    [Route("class")]
    public class ClassController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ClassController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IEnumerable<Class>> Get()
        {
            return await _uow.ClassRepository.GetAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<Class> GetById(Guid id)
        {
            return await _uow.ClassRepository.GetByIdAsync(id);
        }

        [HttpGet("full")]
        public async Task<IEnumerable<Class>> GetFull()
        {
            return await _uow.ClassRepository.GetFullAsync();
        }

        [HttpGet("full/{id:guid}")]
        public async Task<Class> GetFullById(Guid id)
        {
            return await _uow.ClassRepository.GetFullByIdAsync(id);
        }

        [HttpPost("filter")]
        public async Task<IEnumerable<Class>> Filter([FromBody] ClassFilter filter)
        {
            return await _uow.ClassRepository.FilterAsync(filter);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddClassDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Class(model.Name, model.TeacherName);

            _uow.ClassRepository.Add(entity);

            await _uow.CommitAsync();

            return Ok();
        }
        
        [HttpPost("add-student")]
        public async Task<IActionResult> AddStudent([FromBody] ClassStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classEntity = await _uow.ClassRepository.GetFullByIdAsync(model.ClassId, true);
            var studentEntity = await _uow.StudentRepository.GetByIdAsync(model.StudentId);

            if (classEntity is null || studentEntity is null)
            {
                return NotFound();
            }

            classEntity.AddStudent(studentEntity);

            await _uow.CommitAsync();

            return Ok();
        }
        
        [HttpDelete("remove-student")]
        public async Task<IActionResult> RemoveStudent([FromQuery] ClassStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classEntity = await _uow.ClassRepository.GetFullByIdAsync(model.ClassId, true);

            if (classEntity is null)
            {
                return NotFound();
            }

            classEntity.RemoveStudent(model.StudentId);
            
            await _uow.CommitAsync();

            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _uow.ClassRepository.GetByIdAsync(id, true);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Remove();

            _uow.ClassRepository.Update(entity);

            await _uow.CommitAsync();

            return Ok();
        }
    }
}