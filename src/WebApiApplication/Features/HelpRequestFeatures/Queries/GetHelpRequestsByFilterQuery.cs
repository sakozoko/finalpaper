using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.HelpRequestFeatures.Dto;

namespace WebApiApplication.Features.HelpRequestFeatures.Queries;

public class GetHelpRequestsByFilterQuery : IRequest<IEnumerable<HelpRequestDto>>
{
    public string Filter { get; set; } = default!;
    public Guid UserId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public class GetHelpRequestsByFilterQueryValidator : AbstractValidator<GetHelpRequestsByFilterQuery>
    {
        public GetHelpRequestsByFilterQueryValidator()
        {
            RuleFor(x => x.Filter).NotEmpty().Must(filter => !string.IsNullOrWhiteSpace(filter));
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Page).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }

    public class
        GetHelpRequestsByFilterQueryHandler : IRequestHandler<GetHelpRequestsByFilterQuery, IEnumerable<HelpRequestDto>>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public GetHelpRequestsByFilterQueryHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HelpRequestDto>> Handle(GetHelpRequestsByFilterQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Page < 1)
                request.Page = 1;
            if (request.PageSize < 1)
                request.PageSize = 10;
            var helpRequests = await _context.HelpRequests
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.Title.Contains(request.Filter)
                            || x.Description.Contains(request.Filter))
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking()
                .ProjectTo<HelpRequestDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return helpRequests;
        }
    }
}