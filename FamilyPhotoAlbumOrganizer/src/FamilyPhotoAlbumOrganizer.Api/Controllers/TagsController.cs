// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Api.Features.Tags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPhotoAlbumOrganizer.Api.Controllers;

/// <summary>
/// Controller for managing tags.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TagsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TagsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public TagsController(IMediator mediator, ILogger<TagsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all tags.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of tags.</returns>
    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> GetTags([FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        var query = new GetTagsQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new tag.
    /// </summary>
    /// <param name="command">The create tag command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created tag.</returns>
    [HttpPost]
    public async Task<ActionResult<TagDto>> CreateTag([FromBody] CreateTagCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTags), new { id = result.TagId }, result);
    }

    /// <summary>
    /// Deletes a tag.
    /// </summary>
    /// <param name="id">The tag ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTag(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteTagCommand { TagId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
