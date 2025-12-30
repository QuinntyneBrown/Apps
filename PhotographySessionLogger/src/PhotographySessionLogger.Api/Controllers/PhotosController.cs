using PhotographySessionLogger.Api.Features.Photos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PhotographySessionLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PhotosController> _logger;

    public PhotosController(IMediator mediator, ILogger<PhotosController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PhotoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? sessionId,
        [FromQuery] int? minRating)
    {
        _logger.LogInformation("Getting photos for user {UserId}, session {SessionId}", userId, sessionId);

        var result = await _mediator.Send(new GetPhotosQuery
        {
            UserId = userId,
            SessionId = sessionId,
            MinRating = minRating,
        });

        return Ok(result);
    }

    [HttpGet("{photoId:guid}")]
    [ProducesResponseType(typeof(PhotoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PhotoDto>> GetPhotoById(Guid photoId)
    {
        _logger.LogInformation("Getting photo {PhotoId}", photoId);

        var result = await _mediator.Send(new GetPhotoByIdQuery { PhotoId = photoId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PhotoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PhotoDto>> CreatePhoto([FromBody] CreatePhotoCommand command)
    {
        _logger.LogInformation("Creating photo for user {UserId}, session {SessionId}", command.UserId, command.SessionId);

        var result = await _mediator.Send(command);

        return Created($"/api/photos/{result.PhotoId}", result);
    }

    [HttpPut("{photoId:guid}")]
    [ProducesResponseType(typeof(PhotoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PhotoDto>> UpdatePhoto(Guid photoId, [FromBody] UpdatePhotoCommand command)
    {
        if (photoId != command.PhotoId)
        {
            return BadRequest("Photo ID mismatch");
        }

        _logger.LogInformation("Updating photo {PhotoId}", photoId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{photoId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePhoto(Guid photoId)
    {
        _logger.LogInformation("Deleting photo {PhotoId}", photoId);

        var result = await _mediator.Send(new DeletePhotoCommand { PhotoId = photoId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
