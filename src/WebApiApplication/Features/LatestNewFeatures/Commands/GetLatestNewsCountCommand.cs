using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WebApiAbstraction.Repositories;

namespace WebApiApplication.Features.LatestNewFeatures.Commands
{
    public class GetLatestNewsCountCommand : IRequest<int>
    {
        public class GetLatestNewsCountCommandHandler : IRequestHandler<GetLatestNewsCountCommand, int>
        {
            private readonly ILatestNewRepository repository;
            public GetLatestNewsCountCommandHandler(ILatestNewRepository repository)
            {
            this.repository = repository;
            }
            public async Task<int> Handle(GetLatestNewsCountCommand request, CancellationToken cancellationToken)
            {
                return await repository.GetLatestNewsCount();
            }
        }
    }
}