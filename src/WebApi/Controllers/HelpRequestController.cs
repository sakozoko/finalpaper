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
    private readonly UserClaimsHandler _userClaimsRepository;
    private readonly IMediator _mediatr;

    public HelpRequestController(IMediator mediatr, UserClaimsHandler userClaimsRepository)
    {
        _userClaimsRepository = userClaimsRepository;
        _mediatr = mediatr;
    }

    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] CreateHelpRequestInputModel model)
    {
        var userClaims = await _userClaimsRepository.GetClaimsAsync(Request.Headers["Authorization"]!);
        var emailConfirmed = bool.Parse(userClaims.FirstOrDefault(x => x.Type == "email_confirmed")?.Value!);
        var username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value!;
        var userEmail = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value!;
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