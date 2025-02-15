using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.DTOS
{
    public class LogInDto
    {
        [Display(Name ="البريد الالكتروني")]
        public string Email { get; set; }
        [Display(Name = "كلمه السر")]

        public string Password { get; set; }
        [Display(Name = "تذكرني")]

        public bool RememberMe { get; set; }
    }
}
