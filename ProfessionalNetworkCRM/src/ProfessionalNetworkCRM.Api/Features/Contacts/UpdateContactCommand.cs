using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Contacts;

public record UpdateContactCommand : IRequest<ContactDto?>
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
    public List<string>? Tags { get; init; }
    public DateTime? DateMet { get; init; }
    public bool IsPriority { get; init; }
}

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ContactDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<UpdateContactCommandHandler> _logger;

    public UpdateContactCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<UpdateContactCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContactDto?> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating contact {ContactId}",
            request.ContactId);

        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.ContactId == request.ContactId, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Contact {ContactId} not found", request.ContactId);
            return null;
        }

        contact.FirstName = request.FirstName;
        contact.LastName = request.LastName;
        contact.ContactType = request.ContactType;
        contact.Company = request.Company;
        contact.JobTitle = request.JobTitle;
        contact.Email = request.Email;
        contact.Phone = request.Phone;
        contact.LinkedInUrl = request.LinkedInUrl;
        contact.Location = request.Location;
        contact.Notes = request.Notes;
        contact.Tags = request.Tags ?? new List<string>();
        if (request.DateMet.HasValue)
        {
            contact.DateMet = request.DateMet.Value;
        }
        contact.IsPriority = request.IsPriority;
        contact.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated contact {ContactId}", request.ContactId);

        return contact.ToDto();
    }
}
