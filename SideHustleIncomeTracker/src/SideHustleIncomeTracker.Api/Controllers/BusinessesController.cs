using SideHustleIncomeTracker.Api.Features.Businesses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SideHustleIncomeTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BusinessesController> _logger;

    public BusinessesController(IMediator mediator, ILogger<BusinessesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BusinessDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BusinessDto>>> GetBusinesses([FromQuery] bool? isActive)
    {
        _logger.LogInformation("Getting businesses");

        var result = await _mediator.Send(new GetBusinessesQuery { IsActive = isActive });

        return Ok(result);
    }

    [HttpGet("{businessId:guid}")]
    [ProducesResponseType(typeof(BusinessDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BusinessDto>> GetBusinessById(Guid businessId)
    {
        _logger.LogInformation("Getting business {BusinessId}", businessId);

        var result = await _mediator.Send(new GetBusinessByIdQuery { BusinessId = businessId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BusinessDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BusinessDto>> CreateBusiness([FromBody] CreateBusinessCommand command)
    {
        _logger.LogInformation("Creating business: {BusinessName}", command.Name);

        var result = await _mediator.Send(command);

        return Created($"/api/businesses/{result.BusinessId}", result);
    }

    [HttpPut("{businessId:guid}")]
    [ProducesResponseType(typeof(BusinessDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BusinessDto>> UpdateBusiness(Guid businessId, [FromBody] UpdateBusinessCommand command)
    {
        if (businessId != command.BusinessId)
        {
            return BadRequest("Business ID mismatch");
        }

        _logger.LogInformation("Updating business {BusinessId}", businessId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBusiness(Guid businessId)
    {
        _logger.LogInformation("Deleting business {BusinessId}", businessId);

        var result = await _mediator.Send(new DeleteBusinessCommand { BusinessId = businessId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
