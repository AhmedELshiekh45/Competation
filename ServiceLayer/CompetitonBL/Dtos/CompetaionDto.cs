using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.CompetitonBL.Dtos
{
    public class CompetaionDto
    {
        public string? Id { get; set; }
        [Display(Name = "اسم المسابقه"), MaxLength(100), Required]
        public string Name { get; set; }

        [Display(Name = "تاريخ المسابقه"), Required, DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        public int ContestantsNumber { get; set; }
    }
}
