using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.EmergencyContacts;

public record UpdateEmergencyContactCommand : IRequest<EmergencyContactDto?>
{
    public Guid EmergencyContactId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Relationship { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public string? AlternatePhone { get; init; }
    public string? Email { get; init; }
    public string? Address { get; init; }
    public bool IsPrimaryContact { get; init; }
    public string? ContactType { get; init; }
    public string? ServiceArea { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
}

public class UpdateEmergencyContactCommandHandler : IRequestHandler<UpdateEmergencyContactCommand, EmergencyContactDto?>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<UpdateEmergencyContactCommandHandler> _logger;

    public UpdateEmergencyContactCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<UpdateEmergencyContactCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EmergencyContactDto?> Handle(UpdateEmergencyContactCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating emergency contact {EmergencyContactId}", request.EmergencyContactId);

        var contact = await _context.EmergencyContacts
            .FirstOrDefaultAsync(c => c.EmergencyContactId == request.EmergencyContactId, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Emergency contact {EmergencyContactId} not found", request.EmergencyContactId);
            return null;
        }

        contact.Name = request.Name;
        contact.Relationship = request.Relationship;
        contact.PhoneNumber = request.PhoneNumber;
        contact.AlternatePhone = request.AlternatePhone;
        contact.Email = request.Email;
        contact.Address = request.Address;
        contact.IsPrimaryContact = request.IsPrimaryContact;
        contact.ContactType = request.ContactType;
        contact.ServiceArea = request.ServiceArea;
        contact.Notes = request.Notes;
        contact.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated emergency contact {EmergencyContactId}", request.EmergencyContactId);

        return contact.ToDto();
    }
}
