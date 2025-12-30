using BucketListManager.Api.Features.BucketListItems;
using BucketListManager.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BucketListManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BucketListItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BucketListItemsController> _logger;

    public BucketListItemsController(IMediator mediator, ILogger<BucketListItemsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BucketListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BucketListItemDto>>> GetBucketListItems(
        [FromQuery] Guid? userId,
        [FromQuery] Category? category,
        [FromQuery] ItemStatus? status)
    {
        _logger.LogInformation("Getting bucket list items for user {UserId}", userId);

        var result = await _mediator.Send(new GetBucketListItemsQuery
        {
            UserId = userId,
            Category = category,
            Status = status,
        });

        return Ok(result);
    }

    [HttpGet("{bucketListItemId:guid}")]
    [ProducesResponseType(typeof(BucketListItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BucketListItemDto>> GetBucketListItemById(Guid bucketListItemId)
    {
        _logger.LogInformation("Getting bucket list item {BucketListItemId}", bucketListItemId);

        var result = await _mediator.Send(new GetBucketListItemByIdQuery { BucketListItemId = bucketListItemId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BucketListItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BucketListItemDto>> CreateBucketListItem([FromBody] CreateBucketListItemCommand command)
    {
        _logger.LogInformation("Creating bucket list item for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/bucketlistitems/{result.BucketListItemId}", result);
    }

    [HttpPut("{bucketListItemId:guid}")]
    [ProducesResponseType(typeof(BucketListItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BucketListItemDto>> UpdateBucketListItem(
        Guid bucketListItemId,
        [FromBody] UpdateBucketListItemCommand command)
    {
        if (bucketListItemId != command.BucketListItemId)
        {
            return BadRequest("Bucket list item ID mismatch");
        }

        _logger.LogInformation("Updating bucket list item {BucketListItemId}", bucketListItemId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{bucketListItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBucketListItem(Guid bucketListItemId)
    {
        _logger.LogInformation("Deleting bucket list item {BucketListItemId}", bucketListItemId);

        var result = await _mediator.Send(new DeleteBucketListItemCommand { BucketListItemId = bucketListItemId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
