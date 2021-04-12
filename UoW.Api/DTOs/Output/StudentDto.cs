using System;
using System.Collections.Generic;
using UoW.Api.Domain.Entities;

namespace UoW.Api.DTOs.Output
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BrithDate { get; set; }

        public IEnumerable<Class> Classes { get; set; }
    }
}