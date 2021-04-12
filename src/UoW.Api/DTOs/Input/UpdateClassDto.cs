using System;
using System.ComponentModel.DataAnnotations;

namespace UoW.Api.DTOs.Input
{
    public class UpdateClassDto
    {
        [Required] public Guid Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string TeacherName { get; set; }

        [Range(2000, int.MaxValue)]
        public int? Year { get; set; }
    }
}