using FluentValidation;
using MediatR;
using WebApiApplication.Repositories;
using WebApiCore.Models;

namespace WebApiApplication.Features.LatestNewFeatures.Queries;

public class GetLatestNewsByFilterQuery : IRequest<IEnumerable<LatestNew>>
{
    public string Filter { get; set; } = default!;

    public class GetLatestNewsByFilterCommandValidator : AbstractValidator<GetLatestNewsByFilterQuery>
    {
        public GetLatestNewsByFilterCommandValidator()
        {
            RuleFor(x => x.Filter).NotEmpty();
        }
    }

    public class
        GetLatestNewsByFilterCommandHandler : IRequestHandler<GetLatestNewsByFilterQuery, IEnumerable<LatestNew>>
    {
        private readonly ILatestNewRepository _repository;

        public GetLatestNewsByFilterCommandHandler(ILatestNewRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LatestNew>> Handle(GetLatestNewsByFilterQuery command,
            CancellationToken cancellationToken)
        {
            var latestNews = (await _repository.GetLatestNews()).ToList();
            if (!string.IsNullOrWhiteSpace(command.Filter))
                latestNews = latestNews.Where(x => x.Title is not null && x.Title.Contains(command.Filter)).ToList();
            var modifyingLatestNews = latestNews.Where(c => c.Title?.Contains("&#039;") ?? false).ToList();
            var modifiedLatestNews = modifyingLatestNews.Select(n => new LatestNew
            {
                DateTime = n.DateTime,
                Link = n.Link,
                Title = n.Title?.Replace("&#039;", "'")
            });
            return latestNews.Except(modifyingLatestNews).Concat(modifiedLatestNews);
        }
    }
}