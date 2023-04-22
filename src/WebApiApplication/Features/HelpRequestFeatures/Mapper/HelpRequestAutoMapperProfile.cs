using AutoMapper;
using WebApiApplication.Features.HelpRequestFeatures.Commands;
using WebApiApplication.Features.HelpRequestFeatures.Dto;
using WebApiCore.Models;

namespace WebApiApplication.Features.HelpRequestFeatures.Mapper;

public class HelpRequestAutoMapperProfile : Profile
{
    public HelpRequestAutoMapperProfile()
    {
        CreateMap<HelpRequestDto, HelpRequestEntity>();
        CreateMap<HelpRequestEntity, HelpRequestDto>();
        CreateMap<CreateHelpRequestCommand, HelpRequestEntity>()
            .ForMember(c => c.Status, opt => opt.MapFrom(x => HelpRequestStatus.New))
            .ForMember(c => c.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow))
            .ForMember(c => c.UserId, opt => opt.MapFrom(x => x.UserId ?? Guid.Empty))
            .ForMember(c => c.UserEmail, opt => opt.MapFrom(x => x.UserEmail ?? string.Empty))
            .ForMember(c => c.UserName, opt => opt.MapFrom(x => x.Username ?? string.Empty));
    }
}