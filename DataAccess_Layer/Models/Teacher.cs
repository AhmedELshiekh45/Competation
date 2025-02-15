using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Base;

namespace DataAccess_Layer.Models
{
    public class Teacher : BaseClass
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Exam>? ContestantsExams { get; set; } = new List<Exam>();
    }

}
