using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.VetAppointments;

public record DeleteVetAppointmentCommand : IRequest<bool>
{
    public Guid VetAppointmentId { get; init; }
}

public class DeleteVetAppointmentCommandHandler : IRequestHandler<DeleteVetAppointmentCommand, bool>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<DeleteVetAppointmentCommandHandler> _logger;

    public DeleteVetAppointmentCommandHandler(
        IPetCareManagerContext context,
        ILogger<DeleteVetAppointmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteVetAppointmentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting vet appointment {VetAppointmentId}", request.VetAppointmentId);

        var appointment = await _context.VetAppointments
            .FirstOrDefaultAsync(a => a.VetAppointmentId == request.VetAppointmentId, cancellationToken);

        if (appointment == null)
        {
            _logger.LogWarning("Vet appointment {VetAppointmentId} not found", request.VetAppointmentId);
            return false;
        }

        _context.VetAppointments.Remove(appointment);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted vet appointment {VetAppointmentId}", request.VetAppointmentId);

        return true;
    }
}
