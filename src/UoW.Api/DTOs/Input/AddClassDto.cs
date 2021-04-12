using System.ComponentModel.DataAnnotations;

namespace UoW.Api.DTOs.Input
{
    public class AddClassDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string TeacherName { get; set; }
        
        [Range(2000, int.MaxValue)]
        public int? Year { get; set; }
    }
}