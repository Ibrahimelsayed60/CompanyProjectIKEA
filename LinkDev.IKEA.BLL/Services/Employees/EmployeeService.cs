﻿using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(/*IEmployeeRepository employeeRepository*/ IUnitOfWork unitOfWork) // Ask CLR For Creating Object from class implementing "IUnitOfWork"
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeDto> GetEmployees(string search)
        {
            var employees = _unitOfWork.EmployeeRepository
                            .GetIQueryable()
                            .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
                            .Include(E => E.Department)
                            .Select(employee => new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                Department = employee.Department.Name
            }).ToList();

            return employees;

        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);

            if (employee is { })
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType =employee.EmployeeType,
                    Department = employee.Department?.Name,
                };

            return null;
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            _unitOfWork.EmployeeRepository.Add(employee);

            return _unitOfWork.Complete();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.Complete();
        }

        public bool DeleteEmployee(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;

            var employee = employeeRepo.Get(id);

            if(employee is { })
                employeeRepo.Delete(employee);

            return _unitOfWork.Complete() > 0;

        }

        

        
    }
}
