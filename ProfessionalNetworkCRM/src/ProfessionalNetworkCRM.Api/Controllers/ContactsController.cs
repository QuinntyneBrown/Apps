using ProfessionalNetworkCRM.Api.Features.Contacts;
using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalNetworkCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(IMediator mediator, ILogger<ContactsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts(
        [FromQuery] Guid? userId,
        [FromQuery] ContactType? contactType,
        [FromQuery] bool? isPriority,
        [FromQuery] string? tag)
    {
        _logger.LogInformation("Getting contacts for user {UserId}", userId);

        var result = await _mediator.Send(new GetContactsQuery
        {
            UserId = userId,
            ContactType = contactType,
            IsPriority = isPriority,
            Tag = tag,
        });

        return Ok(result);
    }

    [HttpGet("{contactId:guid}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactDto>> GetContactById(Guid contactId)
    {
        _logger.LogInformation("Getting contact {ContactId}", contactId);

        var result = await _mediator.Send(new GetContactByIdQuery { ContactId = contactId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContactDto>> CreateContact([FromBody] CreateContactCommand command)
    {
        _logger.LogInformation("Creating contact for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/contacts/{result.ContactId}", result);
    }

    [HttpPut("{contactId:guid}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactDto>> UpdateContact(Guid contactId, [FromBody] UpdateContactCommand command)
    {
        if (contactId != command.ContactId)
        {
            return BadRequest("Contact ID mismatch");
        }

        _logger.LogInformation("Updating contact {ContactId}", contactId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{contactId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContact(Guid contactId)
    {
        _logger.LogInformation("Deleting contact {ContactId}", contactId);

        var result = await _mediator.Send(new DeleteContactCommand { ContactId = contactId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
