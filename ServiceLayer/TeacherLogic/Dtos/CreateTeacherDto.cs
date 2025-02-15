using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.DTOS
{
    public class CreateTeacherDto
    {
        public string? Id { get; set; }
        [Display(Name="الاسم ")]
        public string Name { get; set; }
    }
}
