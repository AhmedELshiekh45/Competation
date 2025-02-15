using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Base;

namespace DataAccess_Layer.Models
{
    public class Question : BaseClass
    {
        [MaxLength(200)]
        public string? Name { get; set; }


        [Range(0, 10)]
        public float Score { get; set; }
        [ForeignKey("Exam")]
        public string? ExamId { get; set; }
    }

}
