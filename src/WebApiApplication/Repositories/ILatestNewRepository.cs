using WebApiCore.Models;

namespace WebApiAbstraction.Repositories;

public interface ILatestNewRepository
{
    Task<IEnumerable<LatestNew>> GetLatestNews();
    Task<IEnumerable<LatestNew>> GetLatestNewsByPage(int page, int pageSize);
    Task<int> GetLatestNewsCount();
    
}