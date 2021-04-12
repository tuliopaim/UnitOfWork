using System;
using System.Collections.Generic;
using UoW.Api.Domain.Entities;

namespace UoW.Api.DTOs.Output
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string TeacherName { get; set; }
        public int StudentsTotal { get; set; }

        public IEnumerable<StudentDto> Students { get; set; }
    }
}