// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// API controller for managing car modification parts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PartsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PartsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PartsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public PartsController(IMediator mediator, ILogger<PartsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a part by ID.
    /// </summary>
    /// <param name="id">The part ID.</param>
    /// <returns>The part.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting part {PartId}", id);

        var result = await _mediator.Send(new GetPartByIdQuery { PartId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all parts.
    /// </summary>
    /// <returns>The list of parts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PartDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PartDto>>> GetAll()
    {
        _logger.LogInformation("Getting all parts");

        var result = await _mediator.Send(new GetAllPartsQuery());

        return Ok(result);
    }

    /// <summary>
    /// Gets parts by manufacturer.
    /// </summary>
    /// <param name="manufacturer">The manufacturer name.</param>
    /// <returns>The list of parts.</returns>
    [HttpGet("manufacturer/{manufacturer}")]
    [ProducesResponseType(typeof(IEnumerable<PartDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PartDto>>> GetByManufacturer(string manufacturer)
    {
        _logger.LogInformation("Getting parts by manufacturer {Manufacturer}", manufacturer);

        var result = await _mediator.Send(new GetPartsByManufacturerQuery { Manufacturer = manufacturer });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new part.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created part.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PartDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PartDto>> Create([FromBody] CreatePartCommand command)
    {
        _logger.LogInformation("Creating part {Name}", command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.PartId },
            result);
    }

    /// <summary>
    /// Updates an existing part.
    /// </summary>
    /// <param name="id">The part ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated part.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartDto>> Update(Guid id, [FromBody] UpdatePartCommand command)
    {
        if (id != command.PartId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating part {PartId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a part.
    /// </summary>
    /// <param name="id">The part ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting part {PartId}", id);

        var result = await _mediator.Send(new DeletePartCommand { PartId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
