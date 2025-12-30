// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Api.Features.Albums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPhotoAlbumOrganizer.Api.Controllers;

/// <summary>
/// Controller for managing albums.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AlbumsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlbumsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public AlbumsController(IMediator mediator, ILogger<AlbumsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all albums.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of albums.</returns>
    [HttpGet]
    public async Task<ActionResult<List<AlbumDto>>> GetAlbums([FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        var query = new GetAlbumsQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets an album by ID.
    /// </summary>
    /// <param name="id">The album ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The album.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AlbumDto>> GetAlbum(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetAlbumByIdQuery { AlbumId = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new album.
    /// </summary>
    /// <param name="command">The create album command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created album.</returns>
    [HttpPost]
    public async Task<ActionResult<AlbumDto>> CreateAlbum([FromBody] CreateAlbumCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAlbum), new { id = result.AlbumId }, result);
    }

    /// <summary>
    /// Updates an album.
    /// </summary>
    /// <param name="id">The album ID.</param>
    /// <param name="command">The update album command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated album.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<AlbumDto>> UpdateAlbum(Guid id, [FromBody] UpdateAlbumCommand command, CancellationToken cancellationToken)
    {
        if (id != command.AlbumId)
        {
            return BadRequest("Album ID mismatch.");
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
    /// Deletes an album.
    /// </summary>
    /// <param name="id">The album ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAlbum(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteAlbumCommand { AlbumId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
