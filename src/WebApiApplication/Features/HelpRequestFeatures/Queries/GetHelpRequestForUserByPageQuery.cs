using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.HelpRequestFeatures.Dto;

namespace WebApiApplication.Features.HelpRequestFeatures.Queries;

public class GetHelpRequestForUserByPageQuery : IRequest<IEnumerable<HelpRequestDto>>
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public Guid? UserId { get; set; }

    public class GetHelpRequestForUserByPageQueryValidator : AbstractValidator<GetHelpRequestForUserByPageQuery>
    {
        public GetHelpRequestForUserByPageQueryValidator()
        {
            RuleFor(x => x.Page).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }

    public class
        GetHelpRequestForUserByPageQueryHandler : IRequestHandler<GetHelpRequestForUserByPageQuery,
            IEnumerable<HelpRequestDto>>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public GetHelpRequestForUserByPageQueryHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HelpRequestDto>> Handle(GetHelpRequestForUserByPageQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Page < 1) request.Page = 1;
            if (request.PageSize < 1) request.PageSize = 10;
            var helpRequests = await _context.HelpRequests
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.Page!.Value - 1) * request.PageSize!.Value)
                .Take(request.PageSize.Value)
                .AsNoTracking()
                .ProjectTo<HelpRequestDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return helpRequests;
        }
    }
}