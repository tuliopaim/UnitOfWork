using System;
using System.ComponentModel.DataAnnotations;

namespace UoW.Api.DTOs.Input
{
    public class UpdateStudentDto
    {
        [Required] public Guid Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}