using Contacts.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContactsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmergencyContactDto>>> GetAll() => Ok(await _mediator.Send(new GetEmergencyContactsQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<EmergencyContactDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEmergencyContactByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<EmergencyContactDto>> Create(CreateEmergencyContactCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.EmergencyContactId }, result);
    }
}
