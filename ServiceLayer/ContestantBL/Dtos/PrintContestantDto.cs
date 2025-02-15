﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ContestantBL.Dtos
{
    public class PrintContestantDto
    {
        [Display(Name ="الرقم القومي")]
        public string? Id { get; set; }
        public int SerialNumber { get; set; }

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

        public float PartsNumber { get; set; }


        [Display(Name = "اسم المحفظ ")]
        [MaxLength(50)]
        public string TeacherId { get; set; }

        [Display(Name = "تاريخ الامتحان ")]
        [DataType(DataType.Date)]

        public DateOnly CompetationDate { get; set; }

    }
}

