using Vehicles.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Vehicles.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;
    public VehiclesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAll() => Ok(await _mediator.Send(new GetVehiclesQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetVehicleByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<VehicleDto>> Create(CreateVehicleCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.VehicleId }, result);
    }
}
