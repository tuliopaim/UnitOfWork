using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Filters;
using UoW.Api.Domain.Interfaces;
using UoW.Api.DTOs.Input;
using UoW.Api.DTOs.Output;

namespace UoW.Api.Controllers
{
    [ApiController]
    [Route("class")]
    public class ClassController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ClassController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassDto>> Get()
        {
            var list = await _uow.ClassRepository.GetAsync();

            return list?.Select(_mapper.Map<ClassDto>);
        }

        [HttpGet("{id:guid}")]
        public async Task<ClassDto> GetById(Guid id)
        {
            var entity = await _uow.ClassRepository.GetByIdAsync(id);

            return _mapper.Map<ClassDto>(entity);
        }

        [HttpGet("full")]
        public async Task<IEnumerable<ClassFullDto>> GetFull()
        {
            var list = await _uow.ClassRepository.GetFullAsync();

            return list?.Select(_mapper.Map<ClassFullDto>);
        }

        [HttpGet("full/{id:guid}")]
        public async Task<ClassFullDto> GetFullById(Guid id)
        {
            var entity = await _uow.ClassRepository.GetFullByIdAsync(id);

            return _mapper.Map<ClassFullDto>(entity);
        }

        [HttpPost("filter")]
        public async Task<IEnumerable<ClassFullDto>> Filter([FromBody] ClassFilter filter)
        {
            var list = await _uow.ClassRepository.FilterAsync(filter);

            return list?.Select(_mapper.Map<ClassFullDto>);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddClassDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Class(model.Name, model.TeacherName, model.Year);

            _uow.ClassRepository.Add(entity);

            await _uow.CommitAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClassDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _uow.ClassRepository.GetByIdAsync(model.Id, track: true);

            if (entity == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                entity.AlterClassName(model.Name);
            }

            if (!string.IsNullOrWhiteSpace(model.TeacherName))
            {
                entity.AlterTeacherName(model.TeacherName);
            }

            if (model.Year.HasValue)
            {
                entity.AlterYear(model.Year.Value);
            }

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
            var entity = await _uow.ClassRepository.GetByIdAsync(id, track: true);

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