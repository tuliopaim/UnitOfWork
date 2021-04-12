using System;

namespace UoW.Api.DTOs.Output
{
    public struct StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}