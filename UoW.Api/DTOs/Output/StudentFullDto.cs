using System;
using System.Collections.Generic;
using UoW.Api.Domain.Entities;

namespace UoW.Api.DTOs.Output
{
    public struct StudentFullDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BrithDate { get; set; }

        public IEnumerable<ClassDto> Classes { get; set; }
    }
}