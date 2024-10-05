using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    {
        //public int Id { get; set; }

        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }

    }
}
