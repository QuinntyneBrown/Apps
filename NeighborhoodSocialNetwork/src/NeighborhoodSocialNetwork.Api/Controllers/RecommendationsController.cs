using NeighborhoodSocialNetwork.Api.Features.Recommendations;
using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NeighborhoodSocialNetwork.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RecommendationsController> _logger;

    public RecommendationsController(IMediator mediator, ILogger<RecommendationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecommendationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RecommendationDto>>> GetRecommendations(
        [FromQuery] Guid? neighborId,
        [FromQuery] RecommendationType? recommendationType,
        [FromQuery] int? minRating,
        [FromQuery] string? searchTerm)
    {
        _logger.LogInformation("Getting recommendations");

        var result = await _mediator.Send(new GetRecommendationsQuery
        {
            NeighborId = neighborId,
            RecommendationType = recommendationType,
            MinRating = minRating,
            SearchTerm = searchTerm,
        });

        return Ok(result);
    }

    [HttpGet("{recommendationId:guid}")]
    [ProducesResponseType(typeof(RecommendationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecommendationDto>> GetRecommendationById(Guid recommendationId)
    {
        _logger.LogInformation("Getting recommendation {RecommendationId}", recommendationId);

        var result = await _mediator.Send(new GetRecommendationByIdQuery { RecommendationId = recommendationId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RecommendationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecommendationDto>> CreateRecommendation([FromBody] CreateRecommendationCommand command)
    {
        _logger.LogInformation("Creating recommendation");

        var result = await _mediator.Send(command);

        return Created($"/api/recommendations/{result.RecommendationId}", result);
    }

    [HttpPut("{recommendationId:guid}")]
    [ProducesResponseType(typeof(RecommendationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecommendationDto>> UpdateRecommendation(Guid recommendationId, [FromBody] UpdateRecommendationCommand command)
    {
        if (recommendationId != command.RecommendationId)
        {
            return BadRequest("Recommendation ID mismatch");
        }

        _logger.LogInformation("Updating recommendation {RecommendationId}", recommendationId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{recommendationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecommendation(Guid recommendationId)
    {
        _logger.LogInformation("Deleting recommendation {RecommendationId}", recommendationId);

        var result = await _mediator.Send(new DeleteRecommendationCommand { RecommendationId = recommendationId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
