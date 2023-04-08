using FluentValidation;
using MediatR;
using WebApiApplication.Context;
using WebApiApplication.Repositories;
using WebApiApplication.Shared.Exceptions;
using WebApiCore.Models;

namespace WebApiApplication.Features.VolunteerOrganizationFeatures.Queries;

public class GetVolunteerOrganizationsCountQuery : IRequest<int>
{
    public int CityId { get; set; }
    public int CategoryNumber { get; set; }

    public class GetVolunteerOrganizationsCountQueryValidator : AbstractValidator<GetVolunteerOrganizationsCountQuery>
    {
        public GetVolunteerOrganizationsCountQueryValidator()
        {
            RuleFor(p => p.CityId).NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(p => p.CategoryNumber).NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(c => Enum.IsDefined(typeof(VolunteerOrganizationCategory), c))
                .WithMessage("Category must be selected.");
        }
    }

    public class GetVolunteerOrganizationsCountQueryHandler : IRequestHandler<GetVolunteerOrganizationsCountQuery, int>
    {
        private readonly IVolunteerOrganizationRepository _repository;
        private readonly IWebApiDbContext _context;

        public GetVolunteerOrganizationsCountQueryHandler(IVolunteerOrganizationRepository repository,
            IWebApiDbContext context)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<int> Handle(GetVolunteerOrganizationsCountQuery request, CancellationToken cancellationToken)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == request.CityId);
            if (city == null)
                throw new NotFoundException(nameof(City), request.CityId);
            return await _repository.GetVolunteerOrganizationsCountAsync(
                (VolunteerOrganizationCategory)request.CategoryNumber, city);
        }
    }
}