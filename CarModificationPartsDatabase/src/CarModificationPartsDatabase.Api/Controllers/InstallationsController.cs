// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// API controller for managing modification installations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InstallationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<InstallationsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallationsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public InstallationsController(IMediator mediator, ILogger<InstallationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets an installation by ID.
    /// </summary>
    /// <param name="id">The installation ID.</param>
    /// <returns>The installation.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InstallationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InstallationDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting installation {InstallationId}", id);

        var result = await _mediator.Send(new GetInstallationByIdQuery { InstallationId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all installations.
    /// </summary>
    /// <returns>The list of installations.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InstallationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InstallationDto>>> GetAll()
    {
        _logger.LogInformation("Getting all installations");

        var result = await _mediator.Send(new GetAllInstallationsQuery());

        return Ok(result);
    }

    /// <summary>
    /// Gets installations by modification ID.
    /// </summary>
    /// <param name="modificationId">The modification ID.</param>
    /// <returns>The list of installations.</returns>
    [HttpGet("modification/{modificationId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<InstallationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InstallationDto>>> GetByModificationId(Guid modificationId)
    {
        _logger.LogInformation("Getting installations for modification {ModificationId}", modificationId);

        var result = await _mediator.Send(new GetInstallationsByModificationIdQuery { ModificationId = modificationId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new installation.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created installation.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(InstallationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InstallationDto>> Create([FromBody] CreateInstallationCommand command)
    {
        _logger.LogInformation("Creating installation for modification {ModificationId}", command.ModificationId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.InstallationId },
            result);
    }

    /// <summary>
    /// Updates an existing installation.
    /// </summary>
    /// <param name="id">The installation ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated installation.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(InstallationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InstallationDto>> Update(Guid id, [FromBody] UpdateInstallationCommand command)
    {
        if (id != command.InstallationId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating installation {InstallationId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an installation.
    /// </summary>
    /// <param name="id">The installation ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting installation {InstallationId}", id);

        var result = await _mediator.Send(new DeleteInstallationCommand { InstallationId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
