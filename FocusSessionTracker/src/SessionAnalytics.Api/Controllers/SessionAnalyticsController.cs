using SessionAnalytics.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SessionAnalytics.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionAnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SessionAnalyticsController> _logger;

    public SessionAnalyticsController(IMediator mediator, ILogger<SessionAnalyticsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnalyticsDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AnalyticsDto>>> GetAnalytics([FromQuery] Guid? userId = null)
    {
        _logger.LogInformation("Getting analytics");
        var result = await _mediator.Send(new GetAnalyticsQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{analyticsId:guid}")]
    [ProducesResponseType(typeof(AnalyticsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AnalyticsDto>> GetAnalyticsById(Guid analyticsId)
    {
        _logger.LogInformation("Getting analytics {AnalyticsId}", analyticsId);
        var result = await _mediator.Send(new GetAnalyticsByIdQuery { AnalyticsId = analyticsId });
        if (result == null) return NotFound();
        return Ok(result);
    }
}
