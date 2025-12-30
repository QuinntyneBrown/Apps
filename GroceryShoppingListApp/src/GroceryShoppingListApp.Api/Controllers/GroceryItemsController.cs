// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Api.Features.GroceryItems;
using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroceryShoppingListApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroceryItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroceryItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GroceryItemDto>>> GetGroceryItems(
        [FromQuery] Guid? groceryListId,
        [FromQuery] Category? category)
    {
        var query = new GetGroceryItemsQuery
        {
            GroceryListId = groceryListId,
            Category = category
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroceryItemDto>> GetGroceryItem(Guid id)
    {
        var query = new GetGroceryItemQuery { GroceryItemId = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GroceryItemDto>> CreateGroceryItem([FromBody] CreateGroceryItemCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetGroceryItem), new { id = result.GroceryItemId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GroceryItemDto>> UpdateGroceryItem(Guid id, [FromBody] UpdateGroceryItemCommand command)
    {
        if (id != command.GroceryItemId)
            return BadRequest("ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGroceryItem(Guid id)
    {
        var command = new DeleteGroceryItemCommand { GroceryItemId = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
