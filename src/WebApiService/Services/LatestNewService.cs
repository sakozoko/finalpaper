using WebApiAbstraction.Repositories;
using WebApiAbstraction.Services;
using WebApiCore.Models;

namespace WebApiService.Services;

public class LatestNewService : ILatestNewService
{
    private readonly ILatestNewRepository _latestNewRepository;

    public LatestNewService(ILatestNewRepository latestNewRepository)
    {
        _latestNewRepository = latestNewRepository;
    }

    public async Task<int> GetLatestNewsCount()
    {
        return await _latestNewRepository.GetLatestNewsCount();
    }
    
    public async Task<IEnumerable<LatestNew>> GetLatestNews()
    {
        return await _latestNewRepository.GetLatestNews();
    }

    public async Task<IEnumerable<LatestNew>> GetLatestNewsByFilter(string filter)
    {
        return (await GetLatestNews()).Where(c=>(c.Title?.Contains(filter) ?? false)
                                                || (c.Link?.Contains(filter) ?? false)
                                                || (c.DateTime?.Contains(filter) ?? false));
    }

    public async Task<IEnumerable<LatestNew>> GetLatestNewsByPage(int page, int pageSize)
    {
        return await _latestNewRepository.GetLatestNewsByPage(page, pageSize);
    }
}