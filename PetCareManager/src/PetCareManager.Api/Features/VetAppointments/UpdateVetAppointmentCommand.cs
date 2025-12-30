using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.VetAppointments;

public record UpdateVetAppointmentCommand : IRequest<VetAppointmentDto?>
{
    public Guid VetAppointmentId { get; init; }
    public DateTime AppointmentDate { get; init; }
    public string? VetName { get; init; }
    public string? Reason { get; init; }
    public string? Notes { get; init; }
    public decimal? Cost { get; init; }
}

public class UpdateVetAppointmentCommandHandler : IRequestHandler<UpdateVetAppointmentCommand, VetAppointmentDto?>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<UpdateVetAppointmentCommandHandler> _logger;

    public UpdateVetAppointmentCommandHandler(
        IPetCareManagerContext context,
        ILogger<UpdateVetAppointmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VetAppointmentDto?> Handle(UpdateVetAppointmentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating vet appointment {VetAppointmentId}", request.VetAppointmentId);

        var appointment = await _context.VetAppointments
            .FirstOrDefaultAsync(a => a.VetAppointmentId == request.VetAppointmentId, cancellationToken);

        if (appointment == null)
        {
            _logger.LogWarning("Vet appointment {VetAppointmentId} not found", request.VetAppointmentId);
            return null;
        }

        appointment.AppointmentDate = request.AppointmentDate;
        appointment.VetName = request.VetName;
        appointment.Reason = request.Reason;
        appointment.Notes = request.Notes;
        appointment.Cost = request.Cost;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated vet appointment {VetAppointmentId}", request.VetAppointmentId);

        return appointment.ToDto();
    }
}
