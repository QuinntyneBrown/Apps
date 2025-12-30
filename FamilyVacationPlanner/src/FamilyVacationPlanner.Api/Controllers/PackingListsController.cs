using FamilyVacationPlanner.Api.Features.PackingLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyVacationPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackingListsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PackingListsController> _logger;

    public PackingListsController(IMediator mediator, ILogger<PackingListsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PackingListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PackingListDto>>> GetPackingLists([FromQuery] Guid? tripId)
    {
        _logger.LogInformation("Getting packing list items for trip {TripId}", tripId);

        var result = await _mediator.Send(new GetPackingListsQuery { TripId = tripId });

        return Ok(result);
    }

    [HttpGet("{packingListId:guid}")]
    [ProducesResponseType(typeof(PackingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PackingListDto>> GetPackingListById(Guid packingListId)
    {
        _logger.LogInformation("Getting packing list item {PackingListId}", packingListId);

        var result = await _mediator.Send(new GetPackingListByIdQuery { PackingListId = packingListId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PackingListDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PackingListDto>> CreatePackingList([FromBody] CreatePackingListCommand command)
    {
        _logger.LogInformation("Creating packing list item for trip {TripId}", command.TripId);

        var result = await _mediator.Send(command);

        return Created($"/api/packinglists/{result.PackingListId}", result);
    }

    [HttpPut("{packingListId:guid}")]
    [ProducesResponseType(typeof(PackingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PackingListDto>> UpdatePackingList(Guid packingListId, [FromBody] UpdatePackingListCommand command)
    {
        if (packingListId != command.PackingListId)
        {
            return BadRequest("Packing List ID mismatch");
        }

        _logger.LogInformation("Updating packing list item {PackingListId}", packingListId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{packingListId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePackingList(Guid packingListId)
    {
        _logger.LogInformation("Deleting packing list item {PackingListId}", packingListId);

        var result = await _mediator.Send(new DeletePackingListCommand { PackingListId = packingListId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
