using AutoMapper;
using StaffManagementWebApp.ViewModels;

namespace StaffManagementWebApp.Infrastructure
{
    public class AutoMapperConfigureProfiles : Profile
    {
        public AutoMapperConfigureProfiles()
        {
            CreateMap<DisplayStaffViewModel, EditStaffModel>()
                .ForMember(dest => dest.EditId, opt => opt.MapFrom(src => src.StaffId))
                .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.StaffId));
        }
    }
}
