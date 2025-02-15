using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Models;

namespace ServiceLayer.TeacherLogic.Dtos
{
    public class TeacherStudentsDto
    {
        public List<Exam>? Exams  { get; set; }
    }
}
