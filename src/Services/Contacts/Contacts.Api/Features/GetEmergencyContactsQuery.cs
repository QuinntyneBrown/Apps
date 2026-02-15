using Contacts.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Api.Features;

public record GetEmergencyContactsQuery : IRequest<IEnumerable<EmergencyContactDto>>;

public class GetEmergencyContactsQueryHandler : IRequestHandler<GetEmergencyContactsQuery, IEnumerable<EmergencyContactDto>>
{
    private readonly IContactsDbContext _context;
    public GetEmergencyContactsQueryHandler(IContactsDbContext context) => _context = context;
    public async Task<IEnumerable<EmergencyContactDto>> Handle(GetEmergencyContactsQuery request, CancellationToken ct) => await _context.EmergencyContacts.AsNoTracking().Select(c => c.ToDto()).ToListAsync(ct);
}
