using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Features.HelpRequestFeatures.Commands;
using WebApiApplication.Features.HelpRequestFeatures.Queries;
using IdentityModel.Client;
using WebApi.Features;
using WebApi.InputModels.HelpRequest;

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
    public async Task<IActionResult> Create([FromBody] CreateHelpRequestInputModel model)
    {
        var emailConfirmed = bool.Parse(User.Claims.FirstOrDefault(c=>c.Type == "email_verified")?.Value!);
        var username = User.Claims.FirstOrDefault(c=>c.Type == "name")?.Value!;
        var userEmail = User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.Email)?.Value!;
        var command = new CreateHelpRequestCommand
        {
            Title = model.Title,
            Description = model.Description,
            UserId = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value)
                .FirstOrDefault()!),
            EmailConfirmed = emailConfirmed,
            Username = username,
            UserEmail = userEmail
        };
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
    public async Task<IActionResult> GetCountForUser()
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
    public async Task<IActionResult> SearchForUser([FromQuery] string filter, [FromQuery] int page, [FromQuery] int pageSize)
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
    [Authorize("Admin")]
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll(GetHelpRequestsQuery query)
    {
        var result = await _mediatr.Send(query);
        return Ok(result);
    }
    [Authorize("Admin")]
    [HttpGet("getall/count")]
    public async Task<IActionResult> GetAllCount(GetHelpRequestCountQuery query)
    {
        var result = await _mediatr.Send(query);
        return Ok(result);
    }

    [Authorize("Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(DeleteHelpRequestCommand command)
    {
        var result = await _mediatr.Send(command);
        return Ok(result);
    }
    [Authorize("Admin")]
    [HttpPut("answer")]
    public async Task<IActionResult> Answer([FromBody] AnswerToHelpRequestCommand command)
    {
        var result = await _mediatr.Send(command);
        return Ok(result);
    }

}