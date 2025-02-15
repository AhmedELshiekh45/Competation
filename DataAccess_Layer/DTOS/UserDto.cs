using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.DTOS
{
    public class UserDto
    {
        public string Id { get; set; }
        [Display(Name ="اسم المستخدم ")]
        public string? UserName { get; set; }

        public string Role { get; set; }
        [Display(Name = " عدد الطلاب  ")]

        public int NumberOfContestants { get; set; }
    }
}
