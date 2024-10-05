using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.PL.ViewModels.Departments;
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
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during Creating the department";
                //if (_environment.IsDevelopment())
                //{
                //    message = ex.Message;
                //    return View(departmentDto);
                //}
                //else
                //{
                //    message = "Department is not created";
                //    return View("Error", message);
                //}
                
            }
            ModelState.AddModelError(string.Empty, message);

            return View(departmentDto);
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

        [HttpGet] // Get: /Department/Edit/id
        public IActionResult Edit(int? id)
        {
            if(id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if(department is null)
                return NotFound();

            return View(new DepartmentEditViewModel()
            {
                //Id = id.Value,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });


        }

        [HttpPost] // POST
        //public IActionResult Edit(DepartmentEditViewModel departmentVM)
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            if(!ModelState.IsValid) // Server-side Validation
                return View(departmentVM);

            var message = string.Empty;

            try
            {
                var departmentToUpdate = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };

                var updated = _departmentService.updateDepartment(departmentToUpdate) > 0;

                if (updated)
                    return RedirectToAction(nameof(Index));

                message = "an error has occured during updating the department";
            }
            catch (Exception ex)
            {
                // 1. log system
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";

                
            }
            ModelState.AddModelError(string.Empty, message);

            return View(departmentVM);
        }


    }
}
