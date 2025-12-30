// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Api.Features.Photos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPhotoAlbumOrganizer.Api.Controllers;

/// <summary>
/// Controller for managing photos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PhotosController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PhotosController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public PhotosController(IMediator mediator, ILogger<PhotosController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all photos.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="albumId">Optional album ID filter.</param>
    /// <param name="favoritesOnly">Optional favorites filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of photos.</returns>
    [HttpGet]
    public async Task<ActionResult<List<PhotoDto>>> GetPhotos(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? albumId,
        [FromQuery] bool? favoritesOnly,
        CancellationToken cancellationToken)
    {
        var query = new GetPhotosQuery
        {
            UserId = userId,
            AlbumId = albumId,
            FavoritesOnly = favoritesOnly
        };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a photo by ID.
    /// </summary>
    /// <param name="id">The photo ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The photo.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PhotoDto>> GetPhoto(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPhotoByIdQuery { PhotoId = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new photo.
    /// </summary>
    /// <param name="command">The create photo command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created photo.</returns>
    [HttpPost]
    public async Task<ActionResult<PhotoDto>> CreatePhoto([FromBody] CreatePhotoCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPhoto), new { id = result.PhotoId }, result);
    }

    /// <summary>
    /// Updates a photo.
    /// </summary>
    /// <param name="id">The photo ID.</param>
    /// <param name="command">The update photo command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated photo.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<PhotoDto>> UpdatePhoto(Guid id, [FromBody] UpdatePhotoCommand command, CancellationToken cancellationToken)
    {
        if (id != command.PhotoId)
        {
            return BadRequest("Photo ID mismatch.");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a photo.
    /// </summary>
    /// <param name="id">The photo ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePhoto(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeletePhotoCommand { PhotoId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
