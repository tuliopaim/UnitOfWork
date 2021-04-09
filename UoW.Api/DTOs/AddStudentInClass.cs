using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
