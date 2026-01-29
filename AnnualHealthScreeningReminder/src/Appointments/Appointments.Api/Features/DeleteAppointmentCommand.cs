using Appointments.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Api.Features;

public record DeleteAppointmentCommand(Guid AppointmentId) : IRequest<bool>;

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, bool>
{
    private readonly IAppointmentsDbContext _context;
    private readonly ILogger<DeleteAppointmentCommandHandler> _logger;

    public DeleteAppointmentCommandHandler(IAppointmentsDbContext context, ILogger<DeleteAppointmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == request.AppointmentId, cancellationToken);

        if (appointment == null) return false;

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Appointment deleted: {AppointmentId}", request.AppointmentId);
        return true;
    }
}
