using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.EmergencyContacts;

public record DeleteEmergencyContactCommand : IRequest<bool>
{
    public Guid EmergencyContactId { get; init; }
}

public class DeleteEmergencyContactCommandHandler : IRequestHandler<DeleteEmergencyContactCommand, bool>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<DeleteEmergencyContactCommandHandler> _logger;

    public DeleteEmergencyContactCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<DeleteEmergencyContactCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteEmergencyContactCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting emergency contact {EmergencyContactId}", request.EmergencyContactId);

        var contact = await _context.EmergencyContacts
            .FirstOrDefaultAsync(c => c.EmergencyContactId == request.EmergencyContactId, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Emergency contact {EmergencyContactId} not found", request.EmergencyContactId);
            return false;
        }

        _context.EmergencyContacts.Remove(contact);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted emergency contact {EmergencyContactId}", request.EmergencyContactId);

        return true;
    }
}
