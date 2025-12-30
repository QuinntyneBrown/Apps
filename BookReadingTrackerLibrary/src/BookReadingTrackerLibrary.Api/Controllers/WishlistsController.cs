using BookReadingTrackerLibrary.Api.Features.Wishlists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReadingTrackerLibrary.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<WishlistDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WishlistDto>>> GetWishlists(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isAcquired)
    {
        _logger.LogInformation("Getting wishlist items for user {UserId}", userId);

        var result = await _mediator.Send(new GetWishlistsQuery
        {
            UserId = userId,
            IsAcquired = isAcquired,
        });

        return Ok(result);
    }

    [HttpGet("{wishlistId:guid}")]
    [ProducesResponseType(typeof(WishlistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WishlistDto>> GetWishlistById(Guid wishlistId)
    {
        _logger.LogInformation("Getting wishlist item {WishlistId}", wishlistId);

        var result = await _mediator.Send(new GetWishlistByIdQuery { WishlistId = wishlistId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WishlistDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WishlistDto>> CreateWishlist([FromBody] CreateWishlistCommand command)
    {
        _logger.LogInformation("Creating wishlist item for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/wishlists/{result.WishlistId}", result);
    }

    [HttpPut("{wishlistId:guid}")]
    [ProducesResponseType(typeof(WishlistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WishlistDto>> UpdateWishlist(Guid wishlistId, [FromBody] UpdateWishlistCommand command)
    {
        if (wishlistId != command.WishlistId)
        {
            return BadRequest("Wishlist ID mismatch");
        }

        _logger.LogInformation("Updating wishlist item {WishlistId}", wishlistId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{wishlistId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWishlist(Guid wishlistId)
    {
        _logger.LogInformation("Deleting wishlist item {WishlistId}", wishlistId);

        var result = await _mediator.Send(new DeleteWishlistCommand { WishlistId = wishlistId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
