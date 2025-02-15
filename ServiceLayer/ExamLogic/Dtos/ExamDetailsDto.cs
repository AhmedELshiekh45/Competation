using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Models;
using ServiceLayer.ContestantBL.Dtos;

namespace ServiceLayer.ExamLogic.Dtos
{
    public class ExamDetailsDto
    {
        public string? Id { get; set; }
        public float PartsNumber { get; set; }
        public int SerialNumber { get; set; }
        [Range(1, maximum: 100)]
        public float TotalScore
        {
            set; get;
        }

        public string? ContestantId { get; set; }

        [Display(Name = "تاريخ الامتحان ")]
        public string? TeacherName { get; set; }
        public DateOnly? ExamDate { get; set; }
    }
}
