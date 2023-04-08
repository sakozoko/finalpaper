using MediatR;
using WebApiApplication.Repositories;

namespace WebApiApplication.Features.VolunteerOrganizationFeatures.Queries;

public class GetAvailableCategoriesQuery : IRequest<IEnumerable<string>>
{
    public class GetAvailableCategoriesQueryHandler : IRequestHandler<GetAvailableCategoriesQuery, IEnumerable<string>>
    {
        private readonly IVolunteerOrganizationRepository _repository;

        public GetAvailableCategoriesQueryHandler(IVolunteerOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetAvailableCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetCategoriesAsync();
        }
    }
}