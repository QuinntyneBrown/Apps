// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// API controller for managing vehicle modifications.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ModificationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ModificationsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModificationsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public ModificationsController(IMediator mediator, ILogger<ModificationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a modification by ID.
    /// </summary>
    /// <param name="id">The modification ID.</param>
    /// <returns>The modification.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ModificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModificationDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting modification {ModificationId}", id);

        var result = await _mediator.Send(new GetModificationByIdQuery { ModificationId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all modifications.
    /// </summary>
    /// <returns>The list of modifications.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ModificationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ModificationDto>>> GetAll()
    {
        _logger.LogInformation("Getting all modifications");

        var result = await _mediator.Send(new GetAllModificationsQuery());

        return Ok(result);
    }

    /// <summary>
    /// Gets modifications by category.
    /// </summary>
    /// <param name="category">The modification category.</param>
    /// <returns>The list of modifications.</returns>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(IEnumerable<ModificationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ModificationDto>>> GetByCategory(string category)
    {
        _logger.LogInformation("Getting modifications by category {Category}", category);

        var result = await _mediator.Send(new GetModificationsByCategoryQuery { Category = category });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new modification.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created modification.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ModificationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ModificationDto>> Create([FromBody] CreateModificationCommand command)
    {
        _logger.LogInformation("Creating modification {Name}", command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.ModificationId },
            result);
    }

    /// <summary>
    /// Updates an existing modification.
    /// </summary>
    /// <param name="id">The modification ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated modification.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ModificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModificationDto>> Update(Guid id, [FromBody] UpdateModificationCommand command)
    {
        if (id != command.ModificationId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating modification {ModificationId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a modification.
    /// </summary>
    /// <param name="id">The modification ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting modification {ModificationId}", id);

        var result = await _mediator.Send(new DeleteModificationCommand { ModificationId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
