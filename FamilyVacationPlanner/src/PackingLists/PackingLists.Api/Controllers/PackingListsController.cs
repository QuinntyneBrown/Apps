using PackingLists.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PackingLists.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<PackingListDto>>> GetPackingLists(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPackingListsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PackingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PackingListDto>> GetPackingListById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPackingListByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PackingListDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<PackingListDto>> CreatePackingList(
        [FromBody] CreatePackingListCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPackingListById), new { id = result.PackingListId }, result);
    }

    [HttpPut("{id:guid}/toggle")]
    [ProducesResponseType(typeof(PackingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PackingListDto>> TogglePacked(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new TogglePackedCommand(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePackingList(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeletePackingListCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
