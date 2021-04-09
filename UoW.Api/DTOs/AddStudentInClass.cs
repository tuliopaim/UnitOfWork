using System;
using System.ComponentModel.DataAnnotations;

namespace UoW.Api.DTOs
{
    public class AddStudentInClass
    {
        [Required]
        public Guid ClassId { get; set; }

        [Required]
        public Guid StudentId { get; set; }
    }
}
