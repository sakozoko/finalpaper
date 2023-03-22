using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Features.LatestNewFeatures.Commands;
using WebApiApplication.Features.LatestNewFeatures.Queries;

namespace WebApi.Controllers;

[Route("api")]
public class LatestNewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LatestNewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("latestnews")]
    public async Task<IActionResult> GetLatestNews(GetLatestNewsByPageQuery command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("latestnews/count")]
    public async Task<IActionResult> GetLatestNewsCount(GetLatestNewsCountQuery command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("latestnews/filter")]
    public async Task<IActionResult> GetLatestNewsByFilter(GetLatestNewsByFilterQuery command)
    {
        return Ok(await _mediator.Send(command));
    }
}