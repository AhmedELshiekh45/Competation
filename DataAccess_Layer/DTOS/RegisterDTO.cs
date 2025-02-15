using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.DTOS
{
    public class RegisterDTO
    {
        [Display(Name ="اسم المستخدم")]

        [MaxLength(100)]
        public string UserName { get; set; }

        [Display(Name = " كلمه السر")]
        [DataType(DataType.Password)]
        [MaxLength(100)]
        public string PasswordHash { get; set; }
        [Display(Name = "البريد الالكتروني")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(11)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "رقم  الموبايل")]

        public string PhoneNumber { get; set; }
    }
}
