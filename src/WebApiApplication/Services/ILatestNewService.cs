using WebApiCore.Models;

namespace WebApiApplication.Services;

public interface ILatestNewService
{
    public Task<int> GetLatestNewsCount();
    public Task<IEnumerable<LatestNew>> GetLatestNews();
    public Task<IEnumerable<LatestNew>> GetLatestNewsByFilter(string filter);
    public Task<IEnumerable<LatestNew>> GetLatestNewsByPage(int page, int pageSize);
}