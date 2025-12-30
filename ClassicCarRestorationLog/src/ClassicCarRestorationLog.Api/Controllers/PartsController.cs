// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Api.Features.Parts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassicCarRestorationLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PartsController> _logger;

    public PartsController(IMediator mediator, ILogger<PartsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<PartDto>>> GetParts([FromQuery] Guid? projectId, [FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting parts for project {ProjectId}, user {UserId}", projectId, userId);
        var result = await _mediator.Send(new GetParts.Query { ProjectId = projectId, UserId = userId });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartDto>> GetPart(Guid id)
    {
        _logger.LogInformation("Getting part {PartId}", id);
        var result = await _mediator.Send(new GetPartById.Query { PartId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PartDto>> CreatePart(CreatePart.Command command)
    {
        _logger.LogInformation("Creating part for project {ProjectId}", command.ProjectId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPart), new { id = result.PartId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PartDto>> UpdatePart(Guid id, UpdatePart.Command command)
    {
        if (id != command.PartId)
        {
            return BadRequest("Part ID mismatch");
        }

        _logger.LogInformation("Updating part {PartId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePart(Guid id)
    {
        _logger.LogInformation("Deleting part {PartId}", id);
        var result = await _mediator.Send(new DeletePart.Command { PartId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
