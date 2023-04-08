using MediatR;
using WebApiApplication.Repositories;
using WebApiCore.Models;

namespace WebApiApplication.Features.LatestNewFeatures.Queries;

public class GetLatestNewsQuery : IRequest<IEnumerable<LatestNew>>
{
    public class GetLatestNewsCommandHandler : IRequestHandler<GetLatestNewsQuery, IEnumerable<LatestNew>>
    {
        private readonly ILatestNewRepository _repository;

        public GetLatestNewsCommandHandler(ILatestNewRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<LatestNew>> Handle(GetLatestNewsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetLatestNews();
        }
    }
}