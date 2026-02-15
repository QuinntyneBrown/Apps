using Contacts.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Api.Features;

public record GetEmergencyContactByIdQuery(Guid EmergencyContactId) : IRequest<EmergencyContactDto?>;

public class GetEmergencyContactByIdQueryHandler : IRequestHandler<GetEmergencyContactByIdQuery, EmergencyContactDto?>
{
    private readonly IContactsDbContext _context;
    public GetEmergencyContactByIdQueryHandler(IContactsDbContext context) => _context = context;
    public async Task<EmergencyContactDto?> Handle(GetEmergencyContactByIdQuery request, CancellationToken ct)
    {
        var contact = await _context.EmergencyContacts.AsNoTracking().FirstOrDefaultAsync(c => c.EmergencyContactId == request.EmergencyContactId, ct);
        return contact?.ToDto();
    }
}
