// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Api.Features.PriceHistories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroceryShoppingListApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceHistoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PriceHistoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<PriceHistoryDto>>> GetPriceHistories(
        [FromQuery] Guid? groceryItemId,
        [FromQuery] Guid? storeId)
    {
        var query = new GetPriceHistoriesQuery
        {
            GroceryItemId = groceryItemId,
            StoreId = storeId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PriceHistoryDto>> GetPriceHistory(Guid id)
    {
        var query = new GetPriceHistoryQuery { PriceHistoryId = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PriceHistoryDto>> CreatePriceHistory([FromBody] CreatePriceHistoryCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPriceHistory), new { id = result.PriceHistoryId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PriceHistoryDto>> UpdatePriceHistory(Guid id, [FromBody] UpdatePriceHistoryCommand command)
    {
        if (id != command.PriceHistoryId)
            return BadRequest("ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePriceHistory(Guid id)
    {
        var command = new DeletePriceHistoryCommand { PriceHistoryId = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
