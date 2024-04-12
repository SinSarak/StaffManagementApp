using AutoMapper;
using StaffManagementApp.ApplicationCores.Models.DTOModels;
using StaffManagementApp.Domain.Entities;

namespace StaffManagementApp.Infrastructure
{
    public class AutoMapperConfigureProfiles : Profile
    {
        public AutoMapperConfigureProfiles()
        {
            CreateMap<CreateStaffDTO, Staff>();
            CreateMap<EditStaffDTO, Staff>()
                .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.EditId));
            CreateMap<Staff, DisplayStaffDTO>();
        }
    }
}
