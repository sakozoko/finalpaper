﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Features.VolunteerOrganizationFeatures.Queries;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolunteerOrganizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public VolunteerOrganizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetVolunteerOrganizations([FromQuery] GetVolunteerOrganizationsQuery command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("availableCities")]
    public async Task<IActionResult> GetAvailableCities([FromQuery] GetAvailableCitiesQuery command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("availableCategories")]
    public async Task<IActionResult> GetAvailableCategories([FromQuery] GetAvailableCategoriesQuery command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount([FromQuery] GetVolunteerOrganizationsCountQuery command)
    {
        return Ok(await _mediator.Send(command));
    }
}