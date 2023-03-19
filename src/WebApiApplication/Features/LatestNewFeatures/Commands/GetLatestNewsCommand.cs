using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WebApiAbstraction.Repositories;
using WebApiCore.Models;

namespace WebApiApplication.Features.LatestNewFeatures.Commands
{
    public class GetLatestNewsCommand : IRequest<IEnumerable<LatestNew>>
    {
        public class GetLatestNewsCommandHandler : IRequestHandler<GetLatestNewsCommand, IEnumerable<LatestNew>>
        {
            private readonly ILatestNewRepository repository;
            public GetLatestNewsCommandHandler(ILatestNewRepository repository)
            {
                this.repository = repository;
            }
            public async Task<IEnumerable<LatestNew>> Handle(GetLatestNewsCommand request, CancellationToken cancellationToken)
            {
                return await repository.GetLatestNews();
            }
        }
    }
}