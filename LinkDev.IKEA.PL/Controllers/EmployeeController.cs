using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IDepartmentService? _departmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            IEmployeeService employeeService,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment, 
            ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            //_departmentService = departmentService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        #endregion

        #region Index
        [HttpGet] // Get : /Employee/Index
        public async Task<IActionResult> Index(string search)
        {
            var employees = await _employeeService.GetEmployeesAsync(search);

            return View(employees);
        }
        #endregion

        #region Search
        [HttpGet] // Get : /Employee/Search
        public async Task<IActionResult> Search(string search)
        {
            var employees = await _employeeService.GetEmployeesAsync(search);


            return PartialView("Partials/_EmployeeListPartial", employees);

        } 
        #endregion

        #region Create
        [HttpGet] // Get: /Employee/Get
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost] // Post: /Employee/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeEditViewModel employeeVM)
        {

            if (!ModelState.IsValid)
                return View(employeeVM);

            var message = string.Empty;

            try
            {
                var employeeDto = _mapper.Map<EmployeeEditViewModel, CreatedEmployeeDto>(employeeVM);

                var result = await _employeeService.CreateEmployeeAsync(employeeDto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                else
                {
                    message = "Employee is not created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeDto);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An Error occured during creating Employee";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeeVM);
        } 
        #endregion


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);

        }
        #endregion


        #region Update
        [HttpGet] // Get: /Employee/Edit/id?
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();


            var employeeVM = _mapper.Map<EmployeeDetailsDto, EmployeeEditViewModel>(employee);

            return View(employeeVM);

        }

        [HttpPost] // Post: /Employee/Edit/id?
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeEditViewModel updatedEmployee)
        {
            if (!ModelState.IsValid)
                return View(updatedEmployee);

            var message = string.Empty;

            try
            {
                var employeeDto = _mapper.Map<EmployeeEditViewModel, UpdatedEmployeeDto>(updatedEmployee);
                employeeDto.Id = id;

                var result = await _employeeService.UpdateEmployeeAsync(employeeDto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                message = "an error has occured during updating the Employee";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";
            }

            ModelState.AddModelError(string.Empty, message);

            return View(updatedEmployee);
        }
        #endregion


        #region Delete

        [HttpPost] // Post: /Employee/Delete
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _employeeService.DeleteEmployeeAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "an error occured during Deleting Employee";

            }
            catch (Exception ex)
            {
                // 1. log system
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";
            }

            return RedirectToAction(nameof(Index));

        } 
        #endregion



    }
}
