using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.DTOS;
using ServiceLayer.CompetitonBL.Dtos;

namespace ServiceLayer.ContestantBL.Dtos
{
    public class GetallContestantsDto
    {

        public string? Id { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 10)]
        [Display(Name = "الاسم بالكامل")]
        public string FullName { get; set; }
        [Display(Name = "تاريخ الميلاد")]

        public DateOnly BirthDate { get; set; }

        [MaxLength(11)]
        [Display(Name = " رقم التليفون")]

        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        [Display(Name = "نوع التعليم")]

        public string? Education { get; set; }

        [Display(Name = "عدد الاجزاء ")]
        public float NumberOfParts { get; set; }
        //[Display(Name ="الرقم التسلسلي ")]
        //public int SerialNumber { get; set; }


        [Display(Name = "اسم المحفظ ")]
        [MaxLength(50)]
        public string TeacherId { get; set; }

        [Display(Name = "تاريخ الامتحان ")]

        public DateTime ExamDate { get; set; }

        public virtual List<CreateCompetionDto>? Computaions { get; set; }
        public virtual List<ExamDto>? Exams { get; set; }
        public virtual CreateTeacherDto Teacher { get; set; }
        public float Score { get; set; }
    }
}
