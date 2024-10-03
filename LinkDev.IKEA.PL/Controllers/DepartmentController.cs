using LinkDev.IKEA.BLL.Models.Departments;
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
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
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

        [HttpGet] // Get: /Department/Create
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if(!ModelState.IsValid) // Server-side Validation
                return View(departmentDto);

            var message = string.Empty;

            try
            {
                var result = _departmentService.CreateDepartment(departmentDto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Department is not created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
                // 1. log system
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                if(_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentDto);
                }
                else
                {
                    message = "Department is not created";
                    return View("Error", message);
                }
                
            }
        }

        [HttpGet] // Get : /Department/Details
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if(department is null)
                return NotFound();

            return View(department);
        }
    }
}
