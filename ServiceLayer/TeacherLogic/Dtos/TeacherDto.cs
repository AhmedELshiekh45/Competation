using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.TeacherLogic.Dtos
{
    public class TeacherDto
    {
        public string? Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public int NumberOfContestant { get; set; }
    }
}