using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.PublicNewFeatures.Dto;

namespace WebApiApplication.Features.PublicNewFeatures.Queries;

public class GetPublicNewsByFilterQuery : IRequest<IEnumerable<PublicNewDto>>
{
    public string? Filter { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public class GetPublicNewsByFilterQueryValidator : AbstractValidator<GetPublicNewsByFilterQuery>
    {
        public GetPublicNewsByFilterQueryValidator()
        {
            RuleFor(x => x.Filter)
                .Must(c => !string.IsNullOrWhiteSpace(c))
                .MaximumLength(100);
            RuleFor(x => x.Page).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }
    public class GetPublicNewsByFilterQueryHandler : IRequestHandler<GetPublicNewsByFilterQuery, IEnumerable<PublicNewDto>>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public GetPublicNewsByFilterQueryHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublicNewDto>> Handle(GetPublicNewsByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PublicNews
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Title.Contains(request.Filter!) || x.Description.Contains(request.Filter!)
                || x.Author.Contains(request.Filter!))
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking();

            var result = await query.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<PublicNewDto>>(result);
        }
    }
}