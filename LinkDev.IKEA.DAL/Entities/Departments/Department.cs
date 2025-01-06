using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.Departments
{
    public class Department : ModelBase
    {
        
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public DateOnly CreationDate { get; set; }

        
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
