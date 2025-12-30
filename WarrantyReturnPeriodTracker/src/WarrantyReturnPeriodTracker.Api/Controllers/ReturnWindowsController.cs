using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarrantyReturnPeriodTracker.Api.Features.ReturnWindows;

namespace WarrantyReturnPeriodTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReturnWindowsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReturnWindowsController> _logger;

    public ReturnWindowsController(IMediator mediator, ILogger<ReturnWindowsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReturnWindowDto>>> GetReturnWindows(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all return windows");
        var returnWindows = await _mediator.Send(new GetReturnWindowsQuery(), cancellationToken);
        return Ok(returnWindows);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnWindowDto>> GetReturnWindowById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting return window with ID: {ReturnWindowId}", id);
        var returnWindow = await _mediator.Send(new GetReturnWindowByIdQuery { ReturnWindowId = id }, cancellationToken);

        if (returnWindow == null)
        {
            _logger.LogWarning("Return window with ID {ReturnWindowId} not found", id);
            return NotFound();
        }

        return Ok(returnWindow);
    }

    [HttpPost]
    public async Task<ActionResult<ReturnWindowDto>> CreateReturnWindow(
        CreateReturnWindowCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new return window for purchase: {PurchaseId}", command.PurchaseId);
        var returnWindow = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetReturnWindowById), new { id = returnWindow.ReturnWindowId }, returnWindow);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnWindowDto>> UpdateReturnWindow(
        Guid id,
        UpdateReturnWindowCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ReturnWindowId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating return window with ID: {ReturnWindowId}", id);

        try
        {
            var returnWindow = await _mediator.Send(command, cancellationToken);
            return Ok(returnWindow);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to update return window with ID {ReturnWindowId}", id);
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReturnWindow(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting return window with ID: {ReturnWindowId}", id);

        try
        {
            await _mediator.Send(new DeleteReturnWindowCommand { ReturnWindowId = id }, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to delete return window with ID {ReturnWindowId}", id);
            return NotFound(ex.Message);
        }
    }
}
