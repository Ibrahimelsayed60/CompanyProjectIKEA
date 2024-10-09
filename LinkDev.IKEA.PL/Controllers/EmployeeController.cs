using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        #endregion

        #region Index
        [HttpGet] // Get : /Employee/Index
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();

            return View(employees);
        }
        #endregion

        #region Create
        [HttpGet] // Get: /Employee/Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Post: /Employee/Create
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {

            if (!ModelState.IsValid)
                return View(employeeDto);

            var message = string.Empty;

            try
            {
                var result = _employeeService.CreateEmployee(employeeDto);

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
            return View(employeeDto);
        } 
        #endregion


        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);

        }
        #endregion


        #region Update
        [HttpGet] // Get: /Employee/Edit/id?
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            return View(new UpdatedEmployeeDto()
            {
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                Salary = employee.Salary,
                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
            });
        }

        [HttpPost] // Post: /Employee/Edit/id?
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto updatedEmployee)
        {
            if (!ModelState.IsValid)
                return View(updatedEmployee);

            var message = string.Empty;

            try
            {

                var result = _employeeService.UpdateEmployee(updatedEmployee);

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
        //[HttpGet] // Get: /Employee/Delete/id?
        //public IActionResult Delete(int? id)
        //{
        //    if (id is null)
        //        return BadRequest();

        //    var employee = _employeeService.GetEmployeeById(id.Value);

        //    if (employee is null)
        //        return NotFound();

        //    return View(employee);
        //}

        [HttpPost] // Post: /Employee/Delete
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);

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
