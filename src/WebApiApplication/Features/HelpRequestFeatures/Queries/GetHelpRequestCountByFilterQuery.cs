using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;

namespace WebApiApplication.Features.HelpRequestFeatures.Queries
{
    public class GetHelpRequestCountByFilterQuery : IRequest<int>
    {
        public string Filter { get; set; } = default!;
        public Guid UserId { get; set; }

        public class GetHelpRequestCountByFilterValidator : AbstractValidator<GetHelpRequestCountByFilterQuery>
        {
            public GetHelpRequestCountByFilterValidator()
            {
                RuleFor(x => x.Filter).NotEmpty().Must(filter=>!string.IsNullOrWhiteSpace(filter));
                RuleFor(x => x.UserId).NotEmpty();
            }
        }
        
        public class GetHelpRequestCountByFilterHandler : IRequestHandler<GetHelpRequestCountByFilterQuery, int>
        {
            private readonly IWebApiDbContext _context;

            public GetHelpRequestCountByFilterHandler(IWebApiDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(GetHelpRequestCountByFilterQuery request, CancellationToken cancellationToken)
            {
                var helpRequestCount = _context.HelpRequests
                    .Where(x => x.UserId == request.UserId)
                    .Where(x => x.Title.Contains(request.Filter)
                    || x.Description.Contains(request.Filter))
                    .CountAsync();
                return await helpRequestCount;
            }
        }
    }
}