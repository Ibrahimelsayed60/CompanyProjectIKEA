using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {

        #region Services
        //[FromServices]
        //public IDepartmentService DepartmentService { get; set; }

        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
                IDepartmentService departmentService,
                IMapper mapper,
                ILogger<DepartmentController> logger, 
                IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet] // GET: /Department/Index
        public async Task<IActionResult> Index()
        {

            var departments = await _departmentService.GetAllDepartmentsAsync();
    
            return View(departments);

        }
        #endregion

        #region Create
        [HttpGet] // Get: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid) // Server-side Validation
                return View(departmentVM);

            var message = string.Empty;

            try
            {
                var departmentToCreate = _mapper.Map<DepartmentViewModel, CreatedDepartmentDto>(departmentVM);

                var created = await _departmentService.CreateDepartmentAsync(departmentToCreate) > 0;

                if (created)
                {
                    TempData["Message"] = "Department is created";

                }
                else
                {
                    TempData["Message"] = "Department is not created";

                    message = "Department is not created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. log system
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during Creating the department";

                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));

            }

        } 
        #endregion

        #region Details
        [HttpGet] // Get : /Department/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }
        #endregion

        #region Update
        [HttpGet] // Get: /Department/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            var departmentVM = _mapper.Map<DepartmentDetailsDto, DepartmentViewModel>(department);

            return View(departmentVM);

        }

        [HttpPost] // POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid) 
                return View(departmentVM);

            var message = string.Empty;

            try
            {

                var departmentToUpdate = _mapper.Map<DepartmentViewModel, UpdatedDepartmentDto>(departmentVM);
                departmentToUpdate.Id = id;

                var updated = await _departmentService.updateDepartmentAsync(departmentToUpdate) > 0;

                if (updated)
                {
                    TempData["Message"] = "Department is updated";

                }
                else
                {
                    TempData["Message"] = "Department is not updated";

                    message = "an error has occured during updating the department";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. log system
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";

                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _departmentService.DeleteDepartmentAsync(id);

                if (deleted)
                {
                    TempData["Message"] = "Department is Deleted";
                }
                else
                {
                    TempData["Message"] = "Department is not Deleted";
                }

                message = "an error has occured during deleting the department";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. log system
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";
                TempData["Message"] = message;
            }

            return RedirectToAction(nameof(Index));

        } 
        #endregion

    }
}
