// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Api.Features.Stories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<StoryDto>>> GetStories([FromQuery] Guid? personId)
    {
        var query = new GetStories.Query { PersonId = personId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoryDto>> GetStoryById(Guid id)
    {
        var query = new GetStoryById.Query { StoryId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<StoryDto>> CreateStory([FromBody] CreateStory.Command command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStoryById), new { id = result.StoryId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StoryDto>> UpdateStory(Guid id, [FromBody] UpdateStory.Command command)
    {
        command.StoryId = id;
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStory(Guid id)
    {
        var command = new DeleteStory.Command { StoryId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
