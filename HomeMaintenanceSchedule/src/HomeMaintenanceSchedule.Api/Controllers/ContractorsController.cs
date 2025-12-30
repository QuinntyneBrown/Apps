using HomeMaintenanceSchedule.Api.Features.Contractors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceSchedule.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractorsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContractorsController> _logger;

    public ContractorsController(IMediator mediator, ILogger<ContractorsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContractorDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContractorDto>>> GetContractors(
        [FromQuery] Guid? userId,
        [FromQuery] string? specialty,
        [FromQuery] bool? isActive)
    {
        _logger.LogInformation("Getting contractors for user {UserId}", userId);

        var result = await _mediator.Send(new GetContractorsQuery
        {
            UserId = userId,
            Specialty = specialty,
            IsActive = isActive,
        });

        return Ok(result);
    }

    [HttpGet("{contractorId:guid}")]
    [ProducesResponseType(typeof(ContractorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContractorDto>> GetContractorById(Guid contractorId)
    {
        _logger.LogInformation("Getting contractor {ContractorId}", contractorId);

        var result = await _mediator.Send(new GetContractorByIdQuery { ContractorId = contractorId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContractorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContractorDto>> CreateContractor([FromBody] CreateContractorCommand command)
    {
        _logger.LogInformation("Creating contractor for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/contractors/{result.ContractorId}", result);
    }

    [HttpPut("{contractorId:guid}")]
    [ProducesResponseType(typeof(ContractorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContractorDto>> UpdateContractor(Guid contractorId, [FromBody] UpdateContractorCommand command)
    {
        if (contractorId != command.ContractorId)
        {
            return BadRequest("Contractor ID mismatch");
        }

        _logger.LogInformation("Updating contractor {ContractorId}", contractorId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{contractorId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContractor(Guid contractorId)
    {
        _logger.LogInformation("Deleting contractor {ContractorId}", contractorId);

        var result = await _mediator.Send(new DeleteContractorCommand { ContractorId = contractorId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
