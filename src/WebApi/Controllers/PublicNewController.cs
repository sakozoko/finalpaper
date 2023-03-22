using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.InputModels.PublicNew;
using WebApiApplication.Features.PublicNewFeatures.Commands;
using WebApiApplication.Features.PublicNewFeatures.Queries;

namespace WebApi.Controllers;


[Route("api/publicnew")]
public class PublicNewController : ControllerBase
{
    private readonly IMediator _mediator;

    public PublicNewController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> Get([FromQuery] GetPublicNewsByPageQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        var query = new GetPublicNewsCountQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] GetPublicNewsByFilterQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("search/count")]
    public async Task<IActionResult> SearchCount([FromQuery] GetPublicNewsCountByFilterQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var query = new GetPublicNewByIdQuery
        {
            Id = id
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] CreatePublicNewInputModel inputModel)
    {
        var command = new CreatePublicNewCommand
        {
            Title = inputModel.Title,
            Description = inputModel.Description,
            ImageUrl = inputModel.ImageUrl,
            UserId = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value)
                .FirstOrDefault()!),
            Author = User.Claims.Where(x=>x.Type=="username").Select(x=>x.Value).FirstOrDefault()!
        };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [Authorize("Admin")]
    [HttpPut("")]
    public async Task<IActionResult> Update([FromBody] UpdatePublicNewInputModel inputModel)
    {
        var command = new UpdatePublicNewCommand()
        {
            Title = inputModel.Title,
            Description = inputModel.Description,
            ImageUrl = inputModel.ImageUrl,
            Id = inputModel.Id,
            CreatedAt = inputModel.CreatedAt,
            Author = inputModel.Author,
            UserId = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value)
                .FirstOrDefault()!),
        };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [Authorize("Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeletePublicNewCommand
        {
            Id = id
        };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
}
