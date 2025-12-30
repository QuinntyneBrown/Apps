using RoadsideAssistanceInfoHub.Api.Features.InsuranceInfos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RoadsideAssistanceInfoHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsuranceInfosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<InsuranceInfosController> _logger;

    public InsuranceInfosController(IMediator mediator, ILogger<InsuranceInfosController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InsuranceInfoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InsuranceInfoDto>>> GetInsuranceInfos(
        [FromQuery] Guid? vehicleId,
        [FromQuery] string? insuranceCompany,
        [FromQuery] bool? includesRoadsideAssistance)
    {
        _logger.LogInformation("Getting insurance infos");

        var result = await _mediator.Send(new GetInsuranceInfosQuery
        {
            VehicleId = vehicleId,
            InsuranceCompany = insuranceCompany,
            IncludesRoadsideAssistance = includesRoadsideAssistance,
        });

        return Ok(result);
    }

    [HttpGet("{insuranceInfoId:guid}")]
    [ProducesResponseType(typeof(InsuranceInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InsuranceInfoDto>> GetInsuranceInfoById(Guid insuranceInfoId)
    {
        _logger.LogInformation("Getting insurance info {InsuranceInfoId}", insuranceInfoId);

        var result = await _mediator.Send(new GetInsuranceInfoByIdQuery { InsuranceInfoId = insuranceInfoId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(InsuranceInfoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InsuranceInfoDto>> CreateInsuranceInfo([FromBody] CreateInsuranceInfoCommand command)
    {
        _logger.LogInformation("Creating insurance info");

        var result = await _mediator.Send(command);

        return Created($"/api/insuranceinfos/{result.InsuranceInfoId}", result);
    }

    [HttpPut("{insuranceInfoId:guid}")]
    [ProducesResponseType(typeof(InsuranceInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InsuranceInfoDto>> UpdateInsuranceInfo(Guid insuranceInfoId, [FromBody] UpdateInsuranceInfoCommand command)
    {
        if (insuranceInfoId != command.InsuranceInfoId)
        {
            return BadRequest("Insurance info ID mismatch");
        }

        _logger.LogInformation("Updating insurance info {InsuranceInfoId}", insuranceInfoId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{insuranceInfoId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteInsuranceInfo(Guid insuranceInfoId)
    {
        _logger.LogInformation("Deleting insurance info {InsuranceInfoId}", insuranceInfoId);

        var result = await _mediator.Send(new DeleteInsuranceInfoCommand { InsuranceInfoId = insuranceInfoId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
