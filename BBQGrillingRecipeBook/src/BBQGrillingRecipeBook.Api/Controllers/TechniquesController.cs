// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Api.Features.Techniques;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BBQGrillingRecipeBook.Api.Controllers;

/// <summary>
/// Controller for managing BBQ grilling techniques.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TechniquesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TechniquesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TechniquesController"/> class.
    /// </summary>
    public TechniquesController(IMediator mediator, ILogger<TechniquesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all techniques.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of techniques.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<TechniqueDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TechniqueDto>>> GetTechniques(
        [FromQuery] Guid? userId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all techniques");
        var query = new GetTechniquesQuery { UserId = userId };
        var techniques = await _mediator.Send(query, cancellationToken);
        return Ok(techniques);
    }

    /// <summary>
    /// Gets a technique by ID.
    /// </summary>
    /// <param name="id">Technique ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Technique details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TechniqueDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TechniqueDto>> GetTechniqueById(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting technique with ID: {TechniqueId}", id);
        var query = new GetTechniqueByIdQuery { TechniqueId = id };
        var technique = await _mediator.Send(query, cancellationToken);

        if (technique == null)
        {
            return NotFound();
        }

        return Ok(technique);
    }

    /// <summary>
    /// Creates a new technique.
    /// </summary>
    /// <param name="command">Create technique command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created technique.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TechniqueDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TechniqueDto>> CreateTechnique(
        [FromBody] CreateTechniqueCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new technique: {TechniqueName}", command.Name);
        var technique = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTechniqueById), new { id = technique.TechniqueId }, technique);
    }

    /// <summary>
    /// Updates an existing technique.
    /// </summary>
    /// <param name="id">Technique ID.</param>
    /// <param name="command">Update technique command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated technique.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TechniqueDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TechniqueDto>> UpdateTechnique(
        Guid id,
        [FromBody] UpdateTechniqueCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TechniqueId)
        {
            return BadRequest("Technique ID mismatch");
        }

        _logger.LogInformation("Updating technique with ID: {TechniqueId}", id);
        var technique = await _mediator.Send(command, cancellationToken);

        if (technique == null)
        {
            return NotFound();
        }

        return Ok(technique);
    }

    /// <summary>
    /// Deletes a technique.
    /// </summary>
    /// <param name="id">Technique ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTechnique(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting technique with ID: {TechniqueId}", id);
        var command = new DeleteTechniqueCommand { TechniqueId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
