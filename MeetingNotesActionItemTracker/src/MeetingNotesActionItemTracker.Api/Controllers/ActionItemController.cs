// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Api.Features.ActionItem;
using Microsoft.AspNetCore.Mvc;

namespace MeetingNotesActionItemTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActionItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActionItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActionItemDto>>> GetActionItems()
    {
        var actionItems = await _mediator.Send(new GetActionItemsQuery());
        return Ok(actionItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActionItemDto>> GetActionItemById(Guid id)
    {
        var actionItem = await _mediator.Send(new GetActionItemByIdQuery(id));

        if (actionItem == null)
        {
            return NotFound();
        }

        return Ok(actionItem);
    }

    [HttpPost]
    public async Task<ActionResult<ActionItemDto>> CreateActionItem([FromBody] CreateActionItemCommand command)
    {
        var actionItem = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetActionItemById), new { id = actionItem.ActionItemId }, actionItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ActionItemDto>> UpdateActionItem(Guid id, [FromBody] UpdateActionItemCommand command)
    {
        if (id != command.ActionItemId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var actionItem = await _mediator.Send(command);
            return Ok(actionItem);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActionItem(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteActionItemCommand(id));
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
