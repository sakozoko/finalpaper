using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.VolunteerOrganizationFeatures.Dto;

namespace WebApiApplication.Features.VolunteerOrganizationFeatures.Queries;

public class GetAvailableCitiesQuery : IRequest<IEnumerable<CityDto>>
{
    public class GetAvailableCitiesQueryHandler : IRequestHandler<GetAvailableCitiesQuery, IEnumerable<CityDto>>
    {
        private readonly IWebApiDbContext _context;

        public GetAvailableCitiesQueryHandler(IWebApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CityDto>> Handle(GetAvailableCitiesQuery request,
            CancellationToken cancellationToken)
        {
            var cities = _context.Cities.AsNoTracking().Select(c => new CityDto(c.Id, c.Name));
            return await cities.ToListAsync(cancellationToken);
        }
    }
}