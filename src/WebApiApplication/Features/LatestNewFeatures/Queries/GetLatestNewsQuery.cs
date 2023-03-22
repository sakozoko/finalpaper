using MediatR;
using WebApiAbstraction.Repositories;
using WebApiCore.Models;

namespace WebApiApplication.Features.LatestNewFeatures.Commands;

public class GetLatestNewsQuery : IRequest<IEnumerable<LatestNew>>
{
    public class GetLatestNewsCommandHandler : IRequestHandler<GetLatestNewsQuery, IEnumerable<LatestNew>>
    {
        private readonly ILatestNewRepository repository;

        public GetLatestNewsCommandHandler(ILatestNewRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<LatestNew>> Handle(GetLatestNewsQuery request,
            CancellationToken cancellationToken)
        {
            return await repository.GetLatestNews();
        }
    }
}