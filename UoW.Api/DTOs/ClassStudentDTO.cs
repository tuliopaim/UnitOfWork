using System;
using System.ComponentModel.DataAnnotations;

namespace UoW.Api.DTOs
{
    public class ClassStudentDto
    {
        [Required]
        public Guid ClassId { get; set; }

        [Required]
        public Guid StudentId { get; set; }
    }
}
