using FluentValidation;
using MediatR;
using WebApiApplication.Repositories;
using WebApiCore.Models;

namespace WebApiApplication.Features.LatestNewFeatures.Queries;

public class GetLatestNewsByPageQuery : IRequest<IEnumerable<LatestNew>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public class GetLatestNewsByPageCommandValidator : AbstractValidator<GetLatestNewsByPageQuery>
    {
        public GetLatestNewsByPageCommandValidator()
        {
            RuleFor(x => x.Page).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }

    public class GetLatestNewsByPageCommandHandler : IRequestHandler<GetLatestNewsByPageQuery, IEnumerable<LatestNew>>
    {
        private readonly ILatestNewRepository _repository;

        public GetLatestNewsByPageCommandHandler(ILatestNewRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LatestNew>> Handle(GetLatestNewsByPageQuery command,
            CancellationToken cancellationToken)
        {
            if (command.Page < 1) command.Page = 1;
            if (command.PageSize < 1) command.PageSize = 10;
            var latestNews = (await _repository.GetLatestNewsByPage(command.Page, command.PageSize)).ToList();
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