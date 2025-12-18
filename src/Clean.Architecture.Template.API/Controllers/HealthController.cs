using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clean.Architecture.Template.Application.Queries.General;

namespace Clean.Architecture.Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IMediator _mediator;

    public HealthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("status")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }
}
