using HtmlAgilityPack;
using WebApiAbstraction.Repositories;
using WebApiCore.Models;

namespace WebApiInfrastructure.Repositories;

public class LatestNewRepository : ILatestNewRepository
{
    private readonly HtmlWeb _htmlWeb;
    private const string RepositoryUrl = "https://nv.ua/ukr/ukraine.html";
    private const int PageCount = 30;
    private const int PageSize = 30;

    public LatestNewRepository(HtmlWeb htmlWeb)
    {
        _htmlWeb = htmlWeb;
    }

    private async Task<IEnumerable<LatestNew>> GetAllNews()
    {
        var allNews = new List<LatestNew>();
        for (var i = 1; i <= PageCount; i++)
        {
            allNews.AddRange(await GetNews(i));
        }

        return allNews;
    }

    private async Task<IEnumerable<LatestNew>> GetNews(int page = 0)
    {
        var htmlDocument = await _htmlWeb.LoadFromWebAsync(RepositoryUrl+ (page>1?$"?page={page}":""));
        var latestNews =
            htmlDocument.DocumentNode.SelectNodes(
                "//div[contains(@class,'page_results')]//a[contains(@class,'row-result-body')]");
        if (latestNews is null)
        {
            return Enumerable.Empty<LatestNew>();
        }
        
        var latestNewsList = latestNews.Select(c => new LatestNew()
        {
            Title = c.SelectSingleNode("div[@class='row-result-body-text']/div[contains(@class,'title')]")?.InnerText,
            DateTime = c.SelectSingleNode("div[@class='row-result-body-text']/div[contains(@class,'additional')]")?.InnerText,
            Link = c.Attributes["href"].Value
        }).ToList();
        return latestNewsList;
    }

    public async Task<IEnumerable<LatestNew>> GetLatestNews()
    {
        return await GetAllNews();
    }

    public async Task<IEnumerable<LatestNew>> GetLatestNewsByPage(int page, int pageSize)
    {
        return (await GetNews((pageSize* (page - 1))/PageSize+1))
            .Skip((pageSize* (page - 1))-(pageSize* (page - 1))/PageSize*PageSize)
            .Take(pageSize);
    }

    public Task<int> GetLatestNewsCount()
    {
        return Task.FromResult(PageCount*PageSize);
    }
}