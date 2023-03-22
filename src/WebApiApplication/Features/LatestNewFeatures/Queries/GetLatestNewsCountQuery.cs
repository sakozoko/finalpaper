using MediatR;
using WebApiAbstraction.Repositories;

namespace WebApiApplication.Features.LatestNewFeatures.Commands;

public class GetLatestNewsCountQuery : IRequest<int>
{
    public class GetLatestNewsCountCommandHandler : IRequestHandler<GetLatestNewsCountQuery, int>
    {
        private readonly ILatestNewRepository repository;

        public GetLatestNewsCountCommandHandler(ILatestNewRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(GetLatestNewsCountQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetLatestNewsCount();
        }
    }
}