using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.HelpRequestFeatures.Dto;
using WebApiCore.Models;

namespace WebApiApplication.Features.HelpRequestFeatures.Queries
{
    public class GetHelpRequestsQuery : IRequest<IEnumerable<HelpRequestDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Status{get;set;}
        
        public class GetHelpRequestsQueryValidator : AbstractValidator<GetHelpRequestsQuery>
        {
            public GetHelpRequestsQueryValidator()
            {
                RuleFor(x => x.Page).NotEmpty();
                RuleFor(x => x.PageSize).NotEmpty();
                RuleFor(x=>x.Status).Must(x=>
                x is null
                || x.ToUpper() == HelpRequestStatus.New.ToString().ToUpper()
                || x.ToUpper() == HelpRequestStatus.Processed.ToString().ToUpper()
                || x.ToUpper() == HelpRequestStatus.Closed.ToString().ToUpper()
                || x.ToUpper() == HelpRequestStatus.Removed.ToString().ToUpper())
                .WithMessage("Status must be null or one of the following: New, Processed, Closed, Removed");
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
                IEnumerable<HelpRequestEntity> helpRequests;
                if(request.Status is null)
                    helpRequests = await _context.HelpRequests
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                else{
                    var status = Enum.Parse<HelpRequestStatus>(request.Status, true);
                    helpRequests = await _context.HelpRequests
                    .Where(x => x.Status == status)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                }
                return _mapper.Map<IEnumerable<HelpRequestDto>>(helpRequests);
            }
        }
    }
}