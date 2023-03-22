using AutoMapper;
using WebApiApplication.Features.PublicNewFeatures.Commands;
using WebApiApplication.Features.PublicNewFeatures.Dto;
using WebApiCore.Models;

namespace WebApiApplication.Features.PublicNewFeatures.Mapper;

public class PublicNewAutoMapperProfile : Profile
{
    public PublicNewAutoMapperProfile()
    {
       CreateMap<PublicNewDto, PublicNewEntity>();
       CreateMap<PublicNewEntity, PublicNewDto>();
       CreateMap<CreatePublicNewCommand, PublicNewEntity>()
           .ForMember(c => c.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow));
       
    }
}