using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;

namespace LinkDev.IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            #region Employee

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
        }

    }
}
