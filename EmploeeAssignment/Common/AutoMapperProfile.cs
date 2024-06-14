using AutoMapper;
using EmploeeAssignment.Dto;
using EmploeeAssignment.Entities;

namespace EmploeeAssignment.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<EmployeeBasEntity, EmployeeBasDto>().ReverseMap();
            CreateMap<IdentityEntity, IdentityDto>().ReverseMap();
            CreateMap<PersonalEntity, PersonalDto>().ReverseMap();
            CreateMap<WorkEntity,WorkInfoDto>().ReverseMap();
        }
    }
}
