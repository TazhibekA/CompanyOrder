using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyOrders
{
    public class Task
    {
   
        public int Id { get; set; }
        public string Content { get; set; }
        public int DepartmentId { get; set; }
        public bool Completed { get; set; }
        public virtual Department Department { get; set; }
        public DateTime SendDateTime { get; set; }
    }
}
