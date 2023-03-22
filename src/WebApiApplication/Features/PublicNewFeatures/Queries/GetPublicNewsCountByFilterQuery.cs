using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;

namespace WebApiApplication.Features.PublicNewFeatures.Queries;

public class GetPublicNewsCountByFilterQuery : IRequest<int>
{
    public string? Filter { get; set; }
    
    public class GetPublicNewsCountByFilterQueryValidator: AbstractValidator<GetPublicNewsCountByFilterQuery>
    {
        public GetPublicNewsCountByFilterQueryValidator()
        {
            RuleFor(x => x.Filter).MaximumLength(100);
        }
    }
    
    public class GetPublicNewsCountByFilterQueryHandler : IRequestHandler<GetPublicNewsCountByFilterQuery, int>
    {
        private readonly IWebApiDbContext _context;
        public GetPublicNewsCountByFilterQueryHandler(IWebApiDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(GetPublicNewsCountByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PublicNews.AsQueryable();
            if (!string.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(x => x.Title.Contains(request.Filter) || x.Description.Contains(request.Filter));
            }
            return await query.CountAsync(cancellationToken);
        }
    }
}