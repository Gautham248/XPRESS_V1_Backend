using AutoMapper;
using XPRESS_V1_Backend.Models;
using XPRESS_V1_Backend.Models.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TravelRequest, TravelRequestDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.TravelTypeName, opt => opt.MapFrom(src => src.TravelType.Description))
            .ForMember(dest => dest.TripTypeName, opt => opt.MapFrom(src => src.TripType.Description))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectName))
            .ForMember(dest => dest.TravelModeName, opt => opt.MapFrom(src => src.TravelMode.Description))
            .ForMember(dest => dest.CurrentStatusName, opt => opt.MapFrom(src => src.CurrentStatus.StatusName))
            .ForMember(dest => dest.SelectedTicketOptionId, opt => opt.MapFrom(src => src.SelectedTicketOptionId));  // correct mapping
    }
}
