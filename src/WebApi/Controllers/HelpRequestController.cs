using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Features.HelpRequestFeatures.Commands;
using WebApiApplication.Features.HelpRequestFeatures.Queries;

namespace WebApi.Controllers;

[Authorize]
[Route("api/helprequest")]
public class HelpRequestController : ControllerBase
{
    private readonly IMediator _mediatr;

    public HelpRequestController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] CreateHelpRequestCommand command)
    {
        var result = await _mediatr.Send(command);
        return Ok(result);
    }

    [HttpGet("")]
    public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int pageSize)
    {
        var query = new GetHelpRequestForUserByPageQuery
        {
            UserId = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value)
                .FirstOrDefault()!),
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediatr.Send(query);
        return Ok(result);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        var query = new GetHelpRequestCountForUserQuery
        {
            UserId = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value)
                .FirstOrDefault()!)
        };
        var result = await _mediatr.Send(query);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string filter, [FromQuery] int page, [FromQuery] int pageSize)
    {
        var query = new GetHelpRequestsByFilterQuery
        {
            UserId = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value)
                .FirstOrDefault()!),
            Filter = filter,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediatr.Send(query);
        return Ok(result);
    }

    [HttpGet("search/count")]
    public async Task<IActionResult> SearchCount([FromQuery] string filter)
    {
        var query = new GetHelpRequestCountByFilterQuery
        {
            UserId = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value)
                .FirstOrDefault()!),
            Filter = filter
        };
        var result = await _mediatr.Send(query);
        return Ok(result);
    }
}