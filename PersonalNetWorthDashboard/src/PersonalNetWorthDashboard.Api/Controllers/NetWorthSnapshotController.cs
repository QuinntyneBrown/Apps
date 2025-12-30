// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Api.Features.NetWorthSnapshot;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalNetWorthDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NetWorthSnapshotController : ControllerBase
{
    private readonly IMediator _mediator;

    public NetWorthSnapshotController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<NetWorthSnapshotDto>>> GetNetWorthSnapshots(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetNetWorthSnapshotsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NetWorthSnapshotDto>> GetNetWorthSnapshotById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetNetWorthSnapshotByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<NetWorthSnapshotDto>> CreateNetWorthSnapshot(
        [FromBody] CreateNetWorthSnapshotCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetNetWorthSnapshotById), new { id = result.NetWorthSnapshotId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NetWorthSnapshotDto>> UpdateNetWorthSnapshot(
        Guid id,
        [FromBody] UpdateNetWorthSnapshotCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.NetWorthSnapshotId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNetWorthSnapshot(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteNetWorthSnapshotCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
