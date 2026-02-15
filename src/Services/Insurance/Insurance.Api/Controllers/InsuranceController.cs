using Insurance.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsuranceController : ControllerBase
{
    private readonly IMediator _mediator;
    public InsuranceController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsuranceInfoDto>>> GetAll() => Ok(await _mediator.Send(new GetInsuranceInfosQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<InsuranceInfoDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetInsuranceInfoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<InsuranceInfoDto>> Create(CreateInsuranceInfoCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.InsuranceInfoId }, result);
    }
}
