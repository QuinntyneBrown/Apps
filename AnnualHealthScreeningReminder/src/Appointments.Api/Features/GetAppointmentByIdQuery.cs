using Appointments.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Api.Features;

public record GetAppointmentByIdQuery(Guid AppointmentId) : IRequest<AppointmentDto?>;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto?>
{
    private readonly IAppointmentsDbContext _context;

    public GetAppointmentByIdQueryHandler(IAppointmentsDbContext context)
    {
        _context = context;
    }

    public async Task<AppointmentDto?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AppointmentId == request.AppointmentId, cancellationToken);

        return appointment?.ToDto();
    }
}
