using ContactManagementApp.Api.Features.Contacts;
using ContactManagementApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts(
        [FromQuery] Guid? userId,
        [FromQuery] ContactType? contactType,
        [FromQuery] bool? isPriority,
        [FromQuery] string? tag)
    {
        return Ok(new List<ContactDto>());
    }

    [HttpGet("{contactId:guid}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactDto>> GetContactById(Guid contactId)
    {
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ContactDto>> CreateContact([FromBody] CreateContactRequest request)
    {
        return Created(string.Empty, new ContactDto());
    }

    [HttpPut("{contactId:guid}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactDto>> UpdateContact(Guid contactId, [FromBody] UpdateContactRequest request)
    {
        return NotFound();
    }

    [HttpDelete("{contactId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContact(Guid contactId)
    {
        return NotFound();
    }
}

public record CreateContactRequest
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public ContactType ContactType { get; init; }
    public string? Company { get; init; }
    public string? JobTitle { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? LinkedInUrl { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
    public List<string> Tags { get; init; } = new List<string>();
    public DateTime DateMet { get; init; }
    public bool IsPriority { get; init; }
}

public record UpdateContactRequest
{
    public Guid ContactId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public ContactType ContactType { get; init; }
    public string? Company { get; init; }
    public string? JobTitle { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? LinkedInUrl { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
    public List<string> Tags { get; init; } = new List<string>();
    public DateTime DateMet { get; init; }
    public bool IsPriority { get; init; }
}
