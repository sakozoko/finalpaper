using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;

namespace WebApiApplication.Features.HelpRequestFeatures.Queries;

public class GetHelpRequestCountForUserQuery : IRequest<int>
{
    public Guid? UserId { get; set; }

    public class GetHelpRequestCountForUserQueryValidator : AbstractValidator<GetHelpRequestCountForUserQuery>
    {
        public GetHelpRequestCountForUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }

    public class GetHelpRequestCountForUserQueryHandler : IRequestHandler<GetHelpRequestCountForUserQuery, int>
    {
        private readonly IWebApiDbContext _context;

        public GetHelpRequestCountForUserQueryHandler(IWebApiDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetHelpRequestCountForUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.HelpRequests.AsNoTracking()
                .CountAsync(x => x.UserId == request.UserId, cancellationToken: cancellationToken);
        }
    }
}