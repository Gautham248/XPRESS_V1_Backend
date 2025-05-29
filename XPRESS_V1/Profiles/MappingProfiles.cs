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
            .ForMember(dest => dest.CurrentStatusName, opt => opt.MapFrom(src => src.CurrentStatus.StatusName));
        //.ForMember(dest => dest.SelectedTicketOptionName, opt => opt.MapFrom(src => src.SelectedTicketOption != null ? src.SelectedTicketOption.AirlineName : null));

        CreateMap<DocumentDto, Passport>()
               .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore for new entities
               .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber))
               .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.PassportIssueDate))
               .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.PassportExpiryDate))
               .ForMember(dest => dest.IssuingCountry, opt => opt.MapFrom(src => src.IssuingCountry))
               .ForMember(dest => dest.DocumentPath, opt => opt.MapFrom(src => src.DocumentPath))
               .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true)) // Default to active
               .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
               .ForMember(dest => dest.IDTypeId, opt => opt.MapFrom(_ => 1)); // Hardcoded for passport

        CreateMap<Passport, DocumentDto>()
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber))
            .ForMember(dest => dest.PassportIssueDate, opt => opt.MapFrom(src => src.IssueDate))
            .ForMember(dest => dest.PassportExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate))
            .ForMember(dest => dest.IssuingCountry, opt => opt.MapFrom(src => src.IssuingCountry))
            .ForMember(dest => dest.IDTypeId, opt => opt.MapFrom(_ => 1));

        CreateMap<DocumentDto, Visa>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.VisaNumber, opt => opt.MapFrom(src => src.VisaNumber))
            .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.VisaIssueDate))
            .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.VisaExpiryDate))
            .ForMember(dest => dest.VisaClass, opt => opt.MapFrom(src => src.VisaClass))
            .ForMember(dest => dest.DocumentPath, opt => opt.MapFrom(src => src.DocumentPath))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IDTypeId, opt => opt.MapFrom(_ => 2));

        CreateMap<Visa, DocumentDto>()
            .ForMember(dest => dest.VisaNumber, opt => opt.MapFrom(src => src.VisaNumber))
            .ForMember(dest => dest.VisaIssueDate, opt => opt.MapFrom(src => src.IssueDate))
            .ForMember(dest => dest.VisaExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate))
            .ForMember(dest => dest.VisaClass, opt => opt.MapFrom(src => src.VisaClass))
            .ForMember(dest => dest.IDTypeId, opt => opt.MapFrom(_ => 2));

        CreateMap<DocumentDto, Aadhar>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AadharNumber, opt => opt.MapFrom(src => src.AadharNumber))
            //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            //.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.DocumentPath, opt => opt.MapFrom(src => src.DocumentPath))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IDTypeId, opt => opt.MapFrom(_ => 3));

        CreateMap<Aadhar, DocumentDto>()
            .ForMember(dest => dest.AadharNumber, opt => opt.MapFrom(src => src.AadharNumber))
            //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            //.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.IDTypeId, opt => opt.MapFrom(_ => 3));
    }
}
