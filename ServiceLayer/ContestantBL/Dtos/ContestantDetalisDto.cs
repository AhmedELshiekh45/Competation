using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Models;

namespace ServiceLayer.ContestantBL.Dtos
{
    public class ContestantDetailsDto
    {
        public string? Id { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 10)]
        [Display(Name = "الاسم بالكامل")]
        public string FullName { get; set; }
        [Display(Name = "تاريخ الميلاد")]

        public DateOnly BirthDate { get; set; }

        [MaxLength(11)]
        [Display(Name = " رقم التليفون"),DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        [Display(Name = "نوع التعليم")]

        public string? Education { get; set; }

        public List<Exam>? Exams { get; set; }
    }
}
