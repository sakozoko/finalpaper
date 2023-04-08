using FluentValidation;
using MediatR;
using WebApiApplication.Context;
using WebApiApplication.Repositories;
using WebApiApplication.Shared.Exceptions;
using WebApiCore.Models;

namespace WebApiApplication.Features.VolunteerOrganizationFeatures.Queries;

public class GetVolunteerOrganizationsQuery : IRequest<IEnumerable<VolunteerOrganization>>
{
    public int CityId { get; set; }
    public int CategoryId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public class GetVolunteerOrganizationsQueryValidator : AbstractValidator<GetVolunteerOrganizationsQuery>
    {
        public GetVolunteerOrganizationsQueryValidator()
        {
            RuleFor(p => p.CityId).NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(c => Enum.IsDefined(typeof(VolunteerOrganizationCategory), c))
                .WithMessage("Category must be selected.");
            RuleFor(p => p.PageNumber).NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(p => p.PageSize).NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }

    public class
        GetVolunteerOrganizationsQueryHandler : IRequestHandler<GetVolunteerOrganizationsQuery,
            IEnumerable<VolunteerOrganization>>
    {
        private readonly IVolunteerOrganizationRepository _repository;
        private readonly IWebApiDbContext _context;

        public GetVolunteerOrganizationsQueryHandler(IVolunteerOrganizationRepository repository,
            IWebApiDbContext context)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IEnumerable<VolunteerOrganization>> Handle(GetVolunteerOrganizationsQuery request,
            CancellationToken cancellationToken)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == request.CityId);
            if (city == null)
                throw new NotFoundException(nameof(City), request.CityId);
            var volunteerOrganizations =
                await _repository.GetVolunteerOrganizationsAsync((VolunteerOrganizationCategory)request.CategoryId,
                    city);
            return volunteerOrganizations.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
        }
    }
}