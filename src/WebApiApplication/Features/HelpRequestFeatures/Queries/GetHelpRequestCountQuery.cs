using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiCore.Models;

namespace WebApiApplication.Features.HelpRequestFeatures.Queries
{
    public class GetHelpRequestCountQuery : IRequest<int>
    {
        public string? Status {get;set;}
        public class GetHelpRequestCountQueryValidator : AbstractValidator<GetHelpRequestCountQuery>{
            public GetHelpRequestCountQueryValidator()
            {
                RuleFor(x=>x.Status).Must(x=>
                x is null
                || x.ToUpper() == HelpRequestStatus.New.ToString().ToUpper()
                || x.ToUpper() == HelpRequestStatus.Processed.ToString().ToUpper()
                || x.ToUpper() == HelpRequestStatus.Closed.ToString().ToUpper()
                || x.ToUpper() == HelpRequestStatus.Removed.ToString().ToUpper())
                .WithMessage("Status must be null or one of the following: New, Processed, Closed, Removed");
            }
        }
        public class GetHelpRequestCountQueryHandler : IRequestHandler<GetHelpRequestCountQuery, int>
        {
            private readonly IWebApiDbContext _context;

            public GetHelpRequestCountQueryHandler(IWebApiDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(GetHelpRequestCountQuery request, CancellationToken cancellationToken)
            {
                if(request.Status is null)
                    return await _context.HelpRequests.AsNoTracking().CountAsync(cancellationToken);
                else{
                    var status = Enum.Parse<HelpRequestStatus>(request.Status, true);
                    return await _context.HelpRequests.Where(x => x.Status == status).AsNoTracking().CountAsync(cancellationToken);
                }
            }
        }
    }
}