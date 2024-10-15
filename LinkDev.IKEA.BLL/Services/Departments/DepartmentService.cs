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

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            //var departments = _departmentRepository.GetAll();

            var departments = await _unitOfWork.DepartmentRepository.GetIQueryable().Where(D => !D.IsDeleted).Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                //Description = department.Description,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToListAsync();

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

        public async Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

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

        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
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

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> updateDepartmentAsync(UpdatedDepartmentDto departmentDto)
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
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;

            var department = await departmentRepo.GetAsync(id);

            if(department is not null)
                departmentRepo.Delete(department);

            return await _unitOfWork.CompleteAsync() > 0;
        }

    }
}
