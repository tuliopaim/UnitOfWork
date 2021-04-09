﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _repository.GetAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<Student> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [HttpGet("full")]
        public async Task<IEnumerable<Student>> GetFull()
        {
            return await _repository.GetFullAsync();
        }

        [HttpGet("full/{id:guid}")]
        public async Task<Student> GetFullById(Guid id)
        {
            return await _repository.GetFullByIdAsync(id);
        }

        [HttpPost("filter")]
        public async Task<IEnumerable<Student>> Filter([FromBody] StudentFilter filter)
        {
            return await _repository.FilterAsync(filter);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Student(model.Name, model.BirthDate);

            _repository.Add(entity);

            await _uow.CommitAsync();

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

            await _uow.CommitAsync();

            return Ok();
        }
    }
}
