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

        //public int CreatedBy { get; set; }

        //public DateTime CreatedOn { get; set; }

        //public int LastModifiedBy { get; set; }

        //public DateTime LastModifiedOn { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        //public string? Description { get; set; } = null!;

        [Display(Name="Date of creation")]
        public DateOnly CreationDate { get; set; }

        //public static explicit operator DepartmentToReturnDto (Department department)
        //{
        //    return new DepartmentToReturnDto
        //    {
        //        Id = department.Id,
        //        Name = department.Name,
        //        Code = department.Code,
        //        Description = department.Description,
        //        CreationDate = department.CreationDate,
        //    };
        //}
    }
}
