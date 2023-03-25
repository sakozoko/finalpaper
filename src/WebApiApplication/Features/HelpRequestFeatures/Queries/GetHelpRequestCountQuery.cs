using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;

namespace WebApiApplication.Features.HelpRequestFeatures.Queries
{
    public class GetHelpRequestCountQuery : IRequest<int>
    {

        public class GetHelpRequestCountQueryHandler : IRequestHandler<GetHelpRequestCountQuery, int>
        {
            private readonly IWebApiDbContext _context;

            public GetHelpRequestCountQueryHandler(IWebApiDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(GetHelpRequestCountQuery request, CancellationToken cancellationToken)
            {
                return await _context.HelpRequests.AsNoTracking().CountAsync(cancellationToken);
            }
        }
    }
}