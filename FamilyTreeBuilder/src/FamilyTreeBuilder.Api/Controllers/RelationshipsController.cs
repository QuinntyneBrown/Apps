// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Api.Features.Relationships;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelationshipsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RelationshipsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RelationshipDto>>> GetRelationships([FromQuery] Guid? personId)
    {
        var query = new GetRelationships.Query { PersonId = personId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RelationshipDto>> GetRelationshipById(Guid id)
    {
        var query = new GetRelationshipById.Query { RelationshipId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RelationshipDto>> CreateRelationship([FromBody] CreateRelationship.Command command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRelationshipById), new { id = result.RelationshipId }, result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRelationship(Guid id)
    {
        var command = new DeleteRelationship.Command { RelationshipId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
