using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Features.LatestNewFeatures.Commands;

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
    public async Task<IActionResult> GetLatestNews(GetLatestNewsByPageCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [HttpGet("latestnews/count")]
    public async Task<IActionResult> GetLatestNewsCount(GetLatestNewsCountCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [HttpGet("latestnews/filter")]
    public async Task<IActionResult> GetLatestNewsByFilter(GetLatestNewsByFilterCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}