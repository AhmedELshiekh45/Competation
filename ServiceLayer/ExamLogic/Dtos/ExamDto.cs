using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Models;
using ServiceLayer.TeacherLogic.Dtos;

namespace DataAccess_Layer.DTOS
{
    public class ExamDto
    {
        public string? Id { get; set; }
        public float PartsNumber { get; set; }
        public int  SerialNumber { get; set; }
        [Range(1, maximum: 100)]
        public float TotalScore
        {
            set; get;
        }

        public string? ContestantId { get; set; }
        [Display(Name ="تاريخ الامتحان ")]
        public DateOnly? ExamDate { get; set; }

        public TeacherDto? Teacher { get; set; }
    }
}
