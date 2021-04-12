using System;

namespace UoW.Api.DTOs.Output
{
    public struct ClassDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string TeacherName { get; set; }
    }
}