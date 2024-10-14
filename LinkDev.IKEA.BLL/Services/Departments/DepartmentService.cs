using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(/*IDepartmentRepository departmentRepository*/IUnitOfWork unitOfWork) // Ask CLR for creating object from class implementing the interface "IDepartmentRepository"
        {
            _unitOfWork = unitOfWork;

            //_departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            //var departments = _departmentRepository.GetAll();

            var departments = _unitOfWork.DepartmentRepository.GetIQueryable().Where(D => !D.IsDeleted).Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                //Description = department.Description,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToList();

            //foreach (var department in departments)
            //{
            //    //yield return (DepartmentToReturnDto)department;
            //    yield return new DepartmentToReturnDto
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Code = department.Code,
            //        Description = department.Description,
            //        CreationDate = department.CreationDate,
            //    };
            //}

            return departments;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(id);

            if(department is not null)
                return new DepartmentDetailsDto()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                };

            return null;
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                //CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            _unitOfWork.DepartmentRepository.Add(department);

            return _unitOfWork.Complete();
        }

        public int updateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            _unitOfWork.DepartmentRepository.Update(department);
            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;

            var department = departmentRepo.Get(id);

            if(department is not null)
                departmentRepo.Delete(department);

            return _unitOfWork.Complete() > 0;
        }

    }
}
