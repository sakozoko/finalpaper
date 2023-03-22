using AutoMapper;
using FluentValidation;
using MediatR;
using WebApiApplication.Context;
using WebApiApplication.Features.PublicNewFeatures.Dto;

namespace WebApiApplication.Features.PublicNewFeatures.Queries;

public class GetPublicNewByIdQuery : IRequest<PublicNewDto>
{
    public Guid Id { get; set; }

    public class GetPublicNewByIdQueryValidator : AbstractValidator<GetPublicNewByIdQuery>
    {
        public GetPublicNewByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    public class GetPublicNewByIdQueryHandler : IRequestHandler<GetPublicNewByIdQuery, PublicNewDto>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public GetPublicNewByIdQueryHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PublicNewDto> Handle(GetPublicNewByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.PublicNews.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            return _mapper.Map<PublicNewDto>(entity);
        }
    }
}