using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.EmergencyContacts;

public record GetEmergencyContactByIdQuery : IRequest<EmergencyContactDto?>
{
    public Guid EmergencyContactId { get; init; }
}

public class GetEmergencyContactByIdQueryHandler : IRequestHandler<GetEmergencyContactByIdQuery, EmergencyContactDto?>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<GetEmergencyContactByIdQueryHandler> _logger;

    public GetEmergencyContactByIdQueryHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<GetEmergencyContactByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EmergencyContactDto?> Handle(GetEmergencyContactByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting emergency contact {EmergencyContactId}", request.EmergencyContactId);

        var contact = await _context.EmergencyContacts
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.EmergencyContactId == request.EmergencyContactId, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Emergency contact {EmergencyContactId} not found", request.EmergencyContactId);
            return null;
        }

        return contact.ToDto();
    }
}
