using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Interfaces;
using UoW.Api.DTOs;

namespace UoW.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentRepository _repository;

        public StudentController(ILogger<StudentController> logger, IStudentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _repository.Get();
        }

        [HttpGet("{id:guid}")]
        public async Task<Student> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [HttpGet("full/{id:guid}")]
        public async Task<Student> GetFullById(Guid id)
        {
            return await _repository.GetFullById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStudentDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Student(model.Name, model.BirthDate);

            _repository.Add(entity);

            await _repository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            _repository.LogicRemove(entity);

            await _repository.SaveChangesAsync();

            return Ok();
        }
    }
}
