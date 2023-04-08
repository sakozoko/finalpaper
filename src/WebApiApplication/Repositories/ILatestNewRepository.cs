using WebApiCore.Models;

namespace WebApiApplication.Repositories;

public interface ILatestNewRepository
{
    Task<IEnumerable<LatestNew>> GetLatestNews();
    Task<IEnumerable<LatestNew>> GetLatestNewsByPage(int page, int pageSize);
    Task<int> GetLatestNewsCount();
}