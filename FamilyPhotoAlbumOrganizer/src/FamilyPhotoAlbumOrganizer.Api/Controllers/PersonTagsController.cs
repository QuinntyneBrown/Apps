// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Api.Features.PersonTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPhotoAlbumOrganizer.Api.Controllers;

/// <summary>
/// Controller for managing person tags.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonTagsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PersonTagsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonTagsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public PersonTagsController(IMediator mediator, ILogger<PersonTagsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all person tags.
    /// </summary>
    /// <param name="photoId">Optional photo ID filter.</param>
    /// <param name="personName">Optional person name filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of person tags.</returns>
    [HttpGet]
    public async Task<ActionResult<List<PersonTagDto>>> GetPersonTags(
        [FromQuery] Guid? photoId,
        [FromQuery] string? personName,
        CancellationToken cancellationToken)
    {
        var query = new GetPersonTagsQuery
        {
            PhotoId = photoId,
            PersonName = personName
        };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new person tag.
    /// </summary>
    /// <param name="command">The create person tag command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created person tag.</returns>
    [HttpPost]
    public async Task<ActionResult<PersonTagDto>> CreatePersonTag([FromBody] CreatePersonTagCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPersonTags), new { id = result.PersonTagId }, result);
    }

    /// <summary>
    /// Deletes a person tag.
    /// </summary>
    /// <param name="id">The person tag ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePersonTag(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeletePersonTagCommand { PersonTagId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
