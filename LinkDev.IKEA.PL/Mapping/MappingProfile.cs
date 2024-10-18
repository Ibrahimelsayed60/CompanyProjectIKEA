using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.PL.ViewModels.Accounts;
using LinkDev.IKEA.PL.ViewModels.Departments;
using LinkDev.IKEA.PL.ViewModels.Employees;
using LinkDev.IKEA.PL.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            #region Employee

            CreateMap<EmployeeEditViewModel, CreatedEmployeeDto>();

            CreateMap<EmployeeDetailsDto, EmployeeEditViewModel>();

            CreateMap<EmployeeEditViewModel, UpdatedEmployeeDto>();

            #endregion

            #region Department

            CreateMap<DepartmentDetailsDto, DepartmentViewModel>()
                /*.ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name))*/
                //.ReverseMap()
                /*.ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name))*/
                ;

            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();
            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();


            #endregion

            #region User

            CreateMap<ApplicationUser, UserViewModel>();

            #endregion

            #region Role
            CreateMap<IdentityRole, RoleViewModel>();
            #endregion
        }

    }
}
