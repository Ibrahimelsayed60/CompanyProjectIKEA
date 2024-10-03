using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Departments
{
    public class UpdatedDepartmentDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public DateOnly CreationDate { get; set; }
    }
}
