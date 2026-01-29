using Businesses.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Businesses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusinessDto>>> GetBusinesses([FromQuery] Guid tenantId, [FromQuery] Guid userId)
    {
        var query = new GetBusinessesQuery(tenantId, userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessDto>> GetBusiness(Guid id, [FromQuery] Guid tenantId)
    {
        var query = new GetBusinessByIdQuery(id, tenantId);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BusinessDto>> CreateBusiness([FromBody] CreateBusinessCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBusiness), new { id = result.BusinessId, tenantId = command.TenantId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BusinessDto>> UpdateBusiness(Guid id, [FromBody] UpdateBusinessCommand command)
    {
        if (id != command.BusinessId) return BadRequest();
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBusiness(Guid id, [FromQuery] Guid tenantId)
    {
        var command = new DeleteBusinessCommand(id, tenantId);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }
}
