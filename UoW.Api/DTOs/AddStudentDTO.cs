using System;
using System.ComponentModel.DataAnnotations;

namespace UoW.Api.DTOs
{
    public class AddStudentDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}