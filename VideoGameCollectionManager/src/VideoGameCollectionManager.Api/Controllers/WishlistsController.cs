// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGameCollectionManager.Api.Features.Wishlists;

namespace VideoGameCollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WishlistsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WishlistsController> _logger;

    public WishlistsController(IMediator mediator, ILogger<WishlistsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<WishlistDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all wishlist items");
        var wishlists = await _mediator.Send(new GetAllWishlistsQuery(), cancellationToken);
        return Ok(wishlists);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WishlistDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wishlist item with id {WishlistId}", id);
        var wishlist = await _mediator.Send(new GetWishlistByIdQuery { WishlistId = id }, cancellationToken);

        if (wishlist == null)
        {
            _logger.LogWarning("Wishlist item with id {WishlistId} not found", id);
            return NotFound();
        }

        return Ok(wishlist);
    }

    [HttpPost]
    public async Task<ActionResult<WishlistDto>> Create([FromBody] CreateWishlistCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new wishlist item: {Title}", command.Title);
        var wishlist = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = wishlist.WishlistId }, wishlist);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WishlistDto>> Update(Guid id, [FromBody] UpdateWishlistCommand command, CancellationToken cancellationToken)
    {
        if (id != command.WishlistId)
        {
            _logger.LogWarning("Wishlist id mismatch: {UrlId} != {CommandId}", id, command.WishlistId);
            return BadRequest("Wishlist ID mismatch");
        }

        _logger.LogInformation("Updating wishlist item with id {WishlistId}", id);
        var wishlist = await _mediator.Send(command, cancellationToken);

        if (wishlist == null)
        {
            _logger.LogWarning("Wishlist item with id {WishlistId} not found", id);
            return NotFound();
        }

        return Ok(wishlist);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting wishlist item with id {WishlistId}", id);
        var result = await _mediator.Send(new DeleteWishlistCommand { WishlistId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Wishlist item with id {WishlistId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
