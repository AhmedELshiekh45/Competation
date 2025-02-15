using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.DTOS;
using DataAccess_Layer.Models;
using ServiceLayer.CompetitonBL.Dtos;
using ServiceLayer.ContestantBL.Dtos;
using ServiceLayer.ExamLogic.Dtos;

namespace ServiceLayer.TeacherLogic.Dtos
{
    public class TeacherDetailsDto
    {
        public string? Id { get; set; }
        [Display(Name = "الاسم ")]
        public string Name { get; set; }

        public CompetaionDto? Competaion { get; set; }

        public List<Exam>? Exams { get; set; }
    }
}
