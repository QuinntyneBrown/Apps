using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.EmergencyContacts;

public record CreateEmergencyContactCommand : IRequest<EmergencyContactDto>
{
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
}

public class CreateEmergencyContactCommandHandler : IRequestHandler<CreateEmergencyContactCommand, EmergencyContactDto>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<CreateEmergencyContactCommandHandler> _logger;

    public CreateEmergencyContactCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<CreateEmergencyContactCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EmergencyContactDto> Handle(CreateEmergencyContactCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating emergency contact: {Name}",
            request.Name);

        var contact = new EmergencyContact
        {
            EmergencyContactId = Guid.NewGuid(),
            Name = request.Name,
            Relationship = request.Relationship,
            PhoneNumber = request.PhoneNumber,
            AlternatePhone = request.AlternatePhone,
            Email = request.Email,
            Address = request.Address,
            IsPrimaryContact = request.IsPrimaryContact,
            ContactType = request.ContactType,
            ServiceArea = request.ServiceArea,
            Notes = request.Notes,
            IsActive = true,
        };

        _context.EmergencyContacts.Add(contact);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created emergency contact {EmergencyContactId}",
            contact.EmergencyContactId);

        return contact.ToDto();
    }
}
