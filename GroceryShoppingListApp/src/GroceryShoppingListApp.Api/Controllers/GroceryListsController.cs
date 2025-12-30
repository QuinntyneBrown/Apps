// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Api.Features.GroceryLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroceryShoppingListApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroceryListsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroceryListsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GroceryListDto>>> GetGroceryLists([FromQuery] Guid? userId)
    {
        var query = new GetGroceryListsQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroceryListDto>> GetGroceryList(Guid id)
    {
        var query = new GetGroceryListQuery { GroceryListId = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GroceryListDto>> CreateGroceryList([FromBody] CreateGroceryListCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetGroceryList), new { id = result.GroceryListId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GroceryListDto>> UpdateGroceryList(Guid id, [FromBody] UpdateGroceryListCommand command)
    {
        if (id != command.GroceryListId)
            return BadRequest("ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGroceryList(Guid id)
    {
        var command = new DeleteGroceryListCommand { GroceryListId = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
