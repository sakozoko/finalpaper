using MediatR;
using WebApiAbstraction.Repositories;

namespace WebApiApplication.Features.LatestNewFeatures.Queries;

public class GetLatestNewsCountQuery : IRequest<int>
{
    public class GetLatestNewsCountCommandHandler : IRequestHandler<GetLatestNewsCountQuery, int>
    {
        private readonly ILatestNewRepository _repository;

        public GetLatestNewsCountCommandHandler(ILatestNewRepository repository)
        {
            this._repository = repository;
        }

        public async Task<int> Handle(GetLatestNewsCountQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetLatestNewsCount();
        }
    }
}