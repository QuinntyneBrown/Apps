using MusicCollectionOrganizer.Api.Features.Albums;
using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MusicCollectionOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AlbumsController> _logger;

    public AlbumsController(IMediator mediator, ILogger<AlbumsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbums(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? artistId,
        [FromQuery] Format? format,
        [FromQuery] Genre? genre,
        [FromQuery] int? releaseYear)
    {
        _logger.LogInformation("Getting albums for user {UserId}", userId);

        var result = await _mediator.Send(new GetAlbumsQuery
        {
            UserId = userId,
            ArtistId = artistId,
            Format = format,
            Genre = genre,
            ReleaseYear = releaseYear,
        });

        return Ok(result);
    }

    [HttpGet("{albumId:guid}")]
    [ProducesResponseType(typeof(AlbumDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AlbumDto>> GetAlbumById(Guid albumId)
    {
        _logger.LogInformation("Getting album {AlbumId}", albumId);

        var result = await _mediator.Send(new GetAlbumByIdQuery { AlbumId = albumId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AlbumDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AlbumDto>> CreateAlbum([FromBody] CreateAlbumCommand command)
    {
        _logger.LogInformation("Creating album for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/albums/{result.AlbumId}", result);
    }

    [HttpPut("{albumId:guid}")]
    [ProducesResponseType(typeof(AlbumDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AlbumDto>> UpdateAlbum(Guid albumId, [FromBody] UpdateAlbumCommand command)
    {
        if (albumId != command.AlbumId)
        {
            return BadRequest("Album ID mismatch");
        }

        _logger.LogInformation("Updating album {AlbumId}", albumId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{albumId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAlbum(Guid albumId)
    {
        _logger.LogInformation("Deleting album {AlbumId}", albumId);

        var result = await _mediator.Send(new DeleteAlbumCommand { AlbumId = albumId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
