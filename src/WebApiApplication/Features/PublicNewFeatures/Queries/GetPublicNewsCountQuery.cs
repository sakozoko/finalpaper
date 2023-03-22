using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;

namespace WebApiApplication.Features.PublicNewFeatures.Queries;

public class GetPublicNewsCountQuery : IRequest<int>
{
    public class GetPublicNewsCountQueryHandler : IRequestHandler<GetPublicNewsCountQuery, int>
    {
        private readonly IWebApiDbContext _context;

        public GetPublicNewsCountQueryHandler(IWebApiDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetPublicNewsCountQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PublicNews
                .Where(x => x.IsDeleted == false)
                .AsNoTracking();

            var result = await query.CountAsync(cancellationToken);

            return result;
        }
    }
}