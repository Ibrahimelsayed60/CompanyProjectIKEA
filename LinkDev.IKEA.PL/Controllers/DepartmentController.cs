using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance: DepartmentController is a controller
    // Composition: DepartmentController has a IDepartmentService
    public class DepartmentController : Controller
    {
        //[FromServices]
        //public IDepartmentService DepartmentService { get; set; }

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            //return View();
            return View(departments);
            //return View("Index");
            //return View("Index", departments);

        }
    }
}
