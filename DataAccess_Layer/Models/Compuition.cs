using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Base;

namespace DataAccess_Layer.Models
{
    public class Competition
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DateOnly Date { get; set; }

        public virtual ICollection<Exam>? Exams { get; set; } = new List<Exam>();
    }

}
