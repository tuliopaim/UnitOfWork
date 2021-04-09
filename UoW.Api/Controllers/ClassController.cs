using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Interfaces;
using UoW.Api.DTOs;

namespace UoW.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _repository;
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _uow;

        public ClassController(IClassRepository repository, IStudentRepository studentRepository, IUnitOfWork uow)
        {
            _repository = repository;
            _studentRepository = studentRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<IEnumerable<Class>> Get()
        {
            return await _repository.Get();
        }

        [HttpGet("{id:guid}")]
        public async Task<Class> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [HttpGet("full")]
        public async Task<IEnumerable<Class>> GetFull()
        {
            return await _repository.GetFull();
        }

        [HttpGet("full/{id:guid}")]
        public async Task<Class> GetFullById(Guid id)
        {
            return await _repository.GetFullById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddClassDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Class(model.Name, model.TeacherName);

            _repository.Add(entity);

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

            var classEntity = await _repository.GetFullById(model.ClassId, true);
            var studentEntity = await _studentRepository.GetByIdAsync(model.StudentId);

            if (classEntity is null || studentEntity is null)
            {
                return NotFound();
            }

            classEntity.AddStudent(studentEntity);

            await _uow.CommitAsync();

            return Ok();
        }

        [HttpPost("remove-student")]
        public async Task<IActionResult> RemoveStudent([FromBody] ClassStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classEntity = await _repository.GetFullById(model.ClassId, true);

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
            var entity = await _repository.GetByIdAsync(id, true);

            if (entity == null)
            {
                return NotFound();
            }

            _repository.LogicRemove(entity);

            await _uow.CommitAsync();

            return Ok();
        }
    }
}