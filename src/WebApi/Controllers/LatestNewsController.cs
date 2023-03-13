using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Services;

namespace WebApi.Controllers;

[Route("api")]
public class LatestNewsController : ControllerBase
{
    private readonly ILatestNewService _latestNewService;

    public LatestNewsController(ILatestNewService latestNewService)
    {
        _latestNewService = latestNewService;
    }
    [HttpGet("latestnews")]
    public async Task<IActionResult> GetLatestNews(int page = 1, int pageSize = 10)
    {
        return Ok(await _latestNewService.GetLatestNewsByPage(page, pageSize));
    }
    [HttpGet("count")]
    public async Task<IActionResult> GetLatestNewsCount()
    {
        return Ok(await _latestNewService.GetLatestNewsCount());
    }
    [HttpGet("filter")]
    public async Task<IActionResult> GetLatestNewsByFilter(string filter)
    {
        return Ok(await _latestNewService.GetLatestNewsByFilter(filter));
    }
}