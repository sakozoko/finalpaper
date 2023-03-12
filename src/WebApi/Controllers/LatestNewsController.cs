using Microsoft.AspNetCore.Mvc;
using WebApiAbstraction.Services;

namespace WebApi.Controllers;

public class LatestNewsController : ControllerBase
{
    private readonly ILatestNewService _latestNewService;

    public LatestNewsController(ILatestNewService latestNewService)
    {
        _latestNewService = latestNewService;
    }
    [HttpGet("api/latestnews")]
    public async Task<IActionResult> GetLatestNews(int page = 1, int pageSize = 10)
    {
        return Ok(await _latestNewService.GetLatestNewsByPage(page, pageSize));
    }
    [HttpGet("api/latestnews/count")]
    public async Task<IActionResult> GetLatestNewsCount()
    {
        return Ok(await _latestNewService.GetLatestNewsCount());
    }
    [HttpGet("api/latestnews/filter")]
    public async Task<IActionResult> GetLatestNewsByFilter(string filter)
    {
        return Ok(await _latestNewService.GetLatestNewsByFilter(filter));
    }
}