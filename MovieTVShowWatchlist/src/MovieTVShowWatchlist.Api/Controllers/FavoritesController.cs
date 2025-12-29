using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieTVShowWatchlist.Api;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FavoritesController> _logger;

    public FavoritesController(IMediator mediator, ILogger<FavoritesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<FavoriteDto>>> GetFavorites(
        [FromQuery] Guid userId,
        [FromQuery] string? contentType,
        [FromQuery] string? category,
        [FromQuery] string? sortBy,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetFavorites.Query(
            userId, contentType, category, sortBy, page, pageSize));
        return Ok(result);
    }

    [HttpPost("{contentId}")]
    public async Task<ActionResult<FavoriteDto>> CreateFavorite(
        [FromQuery] Guid userId,
        Guid contentId,
        [FromBody] CreateFavoriteRequest request)
    {
        try
        {
            var result = await _mediator.Send(new CreateFavorite.Command(
                userId,
                contentId,
                request.ContentType,
                request.FavoriteCategory,
                request.EmotionalSignificance));
            return CreatedAtAction(nameof(GetFavorites), new { userId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{favoriteId}")]
    public async Task<IActionResult> DeleteFavorite(
        [FromQuery] Guid userId,
        Guid favoriteId)
    {
        var result = await _mediator.Send(new DeleteFavorite.Command(userId, favoriteId));
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
