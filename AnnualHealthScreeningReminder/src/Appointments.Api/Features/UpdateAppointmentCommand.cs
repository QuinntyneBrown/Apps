using Appointments.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Api.Features;

public record UpdateAppointmentCommand(
    Guid AppointmentId,
    string ProviderName,
    string AppointmentType,
    DateTime AppointmentDate,
    string? Location,
    string? Notes,
    bool IsCompleted) : IRequest<AppointmentDto?>;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentDto?>
{
    private readonly IAppointmentsDbContext _context;
    private readonly ILogger<UpdateAppointmentCommandHandler> _logger;

    public UpdateAppointmentCommandHandler(IAppointmentsDbContext context, ILogger<UpdateAppointmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AppointmentDto?> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == request.AppointmentId, cancellationToken);

        if (appointment == null) return null;

        appointment.ProviderName = request.ProviderName;
        appointment.AppointmentType = request.AppointmentType;
        appointment.AppointmentDate = request.AppointmentDate;
        appointment.Location = request.Location;
        appointment.Notes = request.Notes;
        appointment.IsCompleted = request.IsCompleted;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Appointment updated: {AppointmentId}", appointment.AppointmentId);

        return appointment.ToDto();
    }
}
