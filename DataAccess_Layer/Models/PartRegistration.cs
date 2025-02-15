using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Base;

namespace DataAccess_Layer.Models
{
    public class PartRegistration
    {
        [MaxLength(50), Required]
        public string Id { get; set; }

        [Required]
        public float NumberOfParts { get; set; } // عدد الأجزاء (الفئة)

        public int SerialNumber { get; set; } // الرقم التسلسلي داخل هذه الفئة


        public DateOnly? ExamDate { get; set; }

    
    }
}
