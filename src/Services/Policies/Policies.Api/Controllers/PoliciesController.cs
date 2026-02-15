using Policies.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Policies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoliciesController : ControllerBase
{
    private readonly IMediator _mediator;
    public PoliciesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PolicyDto>>> GetAll() => Ok(await _mediator.Send(new GetPoliciesQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<PolicyDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPolicyByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PolicyDto>> Create(CreatePolicyCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.PolicyId }, result);
    }
}
