using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Base;

namespace DataAccess_Layer.Models
{
    public class Contestant
    {
        [Display(Name ="الرقم القومي "),MaxLength(14)]
        public string? Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateOnly BirthDate { get; set; }
        [MaxLength(11),DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        public string? Education { get; set; }


        public string? TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }

        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }

}
