using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataAccess_Layer.Models
{
    public class User:IdentityUser
    {
        public virtual List<Exam>? Exams { get; set; }
    }
}
