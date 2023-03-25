using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.HelpRequestFeatures.Dto;

namespace WebApiApplication.Features.HelpRequestFeatures.Queries
{
    public class GetHelpRequestsQuery : IRequest<IEnumerable<HelpRequestDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        
        public class GetHelpRequestsQueryValidator : AbstractValidator<GetHelpRequestsQuery>
        {
            public GetHelpRequestsQueryValidator()
            {
                RuleFor(x => x.Page).NotEmpty();
                RuleFor(x => x.PageSize).NotEmpty();
            }
        }
        public class GetHelpRequestsQueryHandler : IRequestHandler<GetHelpRequestsQuery, IEnumerable<HelpRequestDto>>
        {
            private readonly IWebApiDbContext _context;
            private readonly IMapper _mapper;
            public GetHelpRequestsQueryHandler(IWebApiDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<HelpRequestDto>> Handle(GetHelpRequestsQuery request, CancellationToken cancellationToken)
            {
                if(request.Page <= 0)
                    request.Page = 1;
                if(request.PageSize <= 0)
                    request.PageSize = 10;
                    
                var helpRequests = await _context.HelpRequests
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<HelpRequestDto>>(helpRequests);
            }
        }
    }
}