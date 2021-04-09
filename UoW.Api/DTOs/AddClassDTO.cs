using System.ComponentModel.DataAnnotations;

namespace UoW.Api.DTOs
{
    public class AddClassDTO
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string TeacherName { get; set; }
    }
}