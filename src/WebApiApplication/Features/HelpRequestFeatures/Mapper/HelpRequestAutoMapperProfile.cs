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
            .ForMember(c => c.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow));
    }
}