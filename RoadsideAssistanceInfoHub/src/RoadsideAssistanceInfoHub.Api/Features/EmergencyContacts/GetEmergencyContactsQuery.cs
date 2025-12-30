using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.EmergencyContacts;

public record GetEmergencyContactsQuery : IRequest<IEnumerable<EmergencyContactDto>>
{
    public string? Name { get; init; }
    public string? ContactType { get; init; }
    public bool? IsPrimaryContact { get; init; }
    public bool? IsActive { get; init; }
}

public class GetEmergencyContactsQueryHandler : IRequestHandler<GetEmergencyContactsQuery, IEnumerable<EmergencyContactDto>>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<GetEmergencyContactsQueryHandler> _logger;

    public GetEmergencyContactsQueryHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<GetEmergencyContactsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<EmergencyContactDto>> Handle(GetEmergencyContactsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting emergency contacts");

        var query = _context.EmergencyContacts.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(c => c.Name.Contains(request.Name));
        }

        if (!string.IsNullOrWhiteSpace(request.ContactType))
        {
            query = query.Where(c => c.ContactType != null && c.ContactType.Contains(request.ContactType));
        }

        if (request.IsPrimaryContact.HasValue)
        {
            query = query.Where(c => c.IsPrimaryContact == request.IsPrimaryContact.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(c => c.IsActive == request.IsActive.Value);
        }

        var contacts = await query
            .OrderByDescending(c => c.IsPrimaryContact)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return contacts.Select(c => c.ToDto());
    }
}
