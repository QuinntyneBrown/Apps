using MusicCollectionOrganizer.Api.Features.Artists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MusicCollectionOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtistsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ArtistsController> _logger;

    public ArtistsController(IMediator mediator, ILogger<ArtistsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ArtistDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ArtistDto>>> GetArtists(
        [FromQuery] Guid? userId,
        [FromQuery] string? country,
        [FromQuery] int? formedYear)
    {
        _logger.LogInformation("Getting artists for user {UserId}", userId);

        var result = await _mediator.Send(new GetArtistsQuery
        {
            UserId = userId,
            Country = country,
            FormedYear = formedYear,
        });

        return Ok(result);
    }

    [HttpGet("{artistId:guid}")]
    [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArtistDto>> GetArtistById(Guid artistId)
    {
        _logger.LogInformation("Getting artist {ArtistId}", artistId);

        var result = await _mediator.Send(new GetArtistByIdQuery { ArtistId = artistId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ArtistDto>> CreateArtist([FromBody] CreateArtistCommand command)
    {
        _logger.LogInformation("Creating artist for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/artists/{result.ArtistId}", result);
    }

    [HttpPut("{artistId:guid}")]
    [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArtistDto>> UpdateArtist(Guid artistId, [FromBody] UpdateArtistCommand command)
    {
        if (artistId != command.ArtistId)
        {
            return BadRequest("Artist ID mismatch");
        }

        _logger.LogInformation("Updating artist {ArtistId}", artistId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{artistId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArtist(Guid artistId)
    {
        _logger.LogInformation("Deleting artist {ArtistId}", artistId);

        var result = await _mediator.Send(new DeleteArtistCommand { ArtistId = artistId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
