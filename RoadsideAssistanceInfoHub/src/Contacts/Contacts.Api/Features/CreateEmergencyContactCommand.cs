using Contacts.Core;
using Contacts.Core.Models;
using MediatR;

namespace Contacts.Api.Features;

public record CreateEmergencyContactCommand(Guid TenantId, string Name, string? Relationship, string PhoneNumber, string? AlternatePhone, string? Email, string? Address, bool IsPrimaryContact, string? ContactType, string? ServiceArea, string? Notes) : IRequest<EmergencyContactDto>;

public class CreateEmergencyContactCommandHandler : IRequestHandler<CreateEmergencyContactCommand, EmergencyContactDto>
{
    private readonly IContactsDbContext _context;
    public CreateEmergencyContactCommandHandler(IContactsDbContext context) => _context = context;
    public async Task<EmergencyContactDto> Handle(CreateEmergencyContactCommand request, CancellationToken ct)
    {
        var contact = new EmergencyContact { EmergencyContactId = Guid.NewGuid(), TenantId = request.TenantId, Name = request.Name, Relationship = request.Relationship, PhoneNumber = request.PhoneNumber, AlternatePhone = request.AlternatePhone, Email = request.Email, Address = request.Address, IsPrimaryContact = request.IsPrimaryContact, ContactType = request.ContactType, ServiceArea = request.ServiceArea, Notes = request.Notes, IsActive = true };
        _context.EmergencyContacts.Add(contact);
        await _context.SaveChangesAsync(ct);
        return contact.ToDto();
    }
}
