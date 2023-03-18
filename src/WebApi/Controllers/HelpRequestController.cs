using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using WebApiApplication.Features.HelpRequestFeatures.Commands;

namespace WebApi.Controllers
{
    [Authorize]
    public class HelpRequestController : ControllerBase
    {
        private IMediator _mediatr;

        public HelpRequestController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpPost("api/helprequest")]
        public async Task<IActionResult> Create([FromBody] CreateHelpRequestCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
    }
}