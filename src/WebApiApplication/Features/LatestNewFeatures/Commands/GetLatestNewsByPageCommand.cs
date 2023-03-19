using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WebApiAbstraction.Repositories;
using WebApiCore.Models;

namespace WebApiApplication.Features.LatestNewFeatures.Commands
{
    public class GetLatestNewsByPageCommand : IRequest<IEnumerable<LatestNew>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public class GetLatestNewsByPageCommandValidator : AbstractValidator<GetLatestNewsByPageCommand>
        {
            public GetLatestNewsByPageCommandValidator()
            {
                RuleFor(x => x.Page).NotEmpty();
                RuleFor(x => x.PageSize).NotEmpty();
            }
        }
        public class GetLatestNewsByPageCommandHandler : IRequestHandler<GetLatestNewsByPageCommand, IEnumerable<LatestNew>>
        {
            private readonly ILatestNewRepository _repository;
            public GetLatestNewsByPageCommandHandler(ILatestNewRepository repository)
            {
                _repository = repository;
            }
            public async Task<IEnumerable<LatestNew>> Handle(GetLatestNewsByPageCommand command, CancellationToken cancellationToken)
            {
                if(command.Page < 1 || command.PageSize < 1)
                {
                    command.Page=1;
                    command.PageSize=10;
                }
                return await _repository.GetLatestNewsByPage(command.Page, command.PageSize);
            }
        }
    }
}