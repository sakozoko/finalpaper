using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.PublicNewFeatures.Dto;

namespace WebApiApplication.Features.PublicNewFeatures.Queries;

public class GetPublicNewsByPageQuery : IRequest<IEnumerable<PublicNewDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public class GetPublicNewsByPageQueryValidator : AbstractValidator<GetPublicNewsByPageQuery>
    {
        public GetPublicNewsByPageQueryValidator()
        {
            RuleFor(x => x.Page).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }
    public class GetPublicNewsByPageQueryHandler : IRequestHandler<GetPublicNewsByPageQuery, IEnumerable<PublicNewDto>>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public GetPublicNewsByPageQueryHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublicNewDto>> Handle(GetPublicNewsByPageQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PublicNews
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking();

            var result = await query.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<PublicNewDto>>(result);
        }
    }
}