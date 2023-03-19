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
    public class GetLatestNewsByFilterCommand : IRequest<IEnumerable<LatestNew>>
    {
        public string Filter { get; set; } = default!;
        public class GetLatestNewsByFilterCommandValidator : AbstractValidator<GetLatestNewsByFilterCommand>
        {
            public GetLatestNewsByFilterCommandValidator()
            {
                RuleFor(x => x.Filter).NotEmpty();
            }
        }
        public class GetLatestNewsByFilterCommandHandler : IRequestHandler<GetLatestNewsByFilterCommand, IEnumerable<LatestNew>>
        {
            private readonly ILatestNewRepository _repository;
            public GetLatestNewsByFilterCommandHandler(ILatestNewRepository repository)
            {
                _repository = repository;
            }
            public async Task<IEnumerable<LatestNew>> Handle(GetLatestNewsByFilterCommand command, CancellationToken cancellationToken)
            {
                var latestNews = await  _repository.GetLatestNews();
                if (!string.IsNullOrWhiteSpace(command.Filter))
                {
                    latestNews = latestNews.Where(x => x.Title is not null && x.Title.Contains(command.Filter));
                }
                return latestNews;
            }
        }
    }
    
}