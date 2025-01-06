using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Departments
{
    public class DepartmentDto
    {
        public int Id { get; set; }


        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;


        [Display(Name="Date of creation")]
        public DateOnly CreationDate { get; set; }

    }
}
