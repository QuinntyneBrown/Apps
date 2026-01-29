using Appointments.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Api.Features;

public record GetAppointmentsQuery : IRequest<IEnumerable<AppointmentDto>>;

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentsDbContext _context;

    public GetAppointmentsQueryHandler(IAppointmentsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _context.Appointments
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return appointments.Select(a => a.ToDto());
    }
}
