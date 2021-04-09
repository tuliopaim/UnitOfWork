using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IUnitOfWork _uow;

        public ClassController(IClassRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
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

        [HttpGet("full/{id:guid}")]
        public async Task<Class> GetFullById(Guid id)
        {
            return await _repository.GetFullById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddClassDTO model)
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