// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Api.Features.Stores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroceryShoppingListApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<StoreDto>>> GetStores([FromQuery] Guid? userId)
    {
        var query = new GetStoresQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreDto>> GetStore(Guid id)
    {
        var query = new GetStoreQuery { StoreId = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<StoreDto>> CreateStore([FromBody] CreateStoreCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStore), new { id = result.StoreId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StoreDto>> UpdateStore(Guid id, [FromBody] UpdateStoreCommand command)
    {
        if (id != command.StoreId)
            return BadRequest("ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStore(Guid id)
    {
        var command = new DeleteStoreCommand { StoreId = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
