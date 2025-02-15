using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.DTOS
{
    public class QuestionDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public float    Score { get; set; }
    }
}
