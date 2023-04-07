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
        public string? Status { get; set; }

        public class GetHelpRequestsQueryValidator : AbstractValidator<GetHelpRequestsQuery>
        {
            public GetHelpRequestsQueryValidator()
            {
                RuleFor(x => x.Status).Must(x =>
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

            public async Task<IEnumerable<HelpRequestDto>> Handle(GetHelpRequestsQuery request,
                CancellationToken cancellationToken)
            {
                IEnumerable<HelpRequestEntity> helpRequests;
                if (request.Status is null)
                {
                    helpRequests = await _context.HelpRequests
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                }
                else
                {
                    var status = Enum.Parse<HelpRequestStatus>(request.Status, true);
                    helpRequests = await _context.HelpRequests
                        .Where(x => x.Status == status)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                }

                return _mapper.Map<IEnumerable<HelpRequestDto>>(helpRequests);
            }
        }
    }
}