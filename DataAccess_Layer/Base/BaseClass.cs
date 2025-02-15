using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Base
{
    public class BaseClass
    {
        public BaseClass()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
