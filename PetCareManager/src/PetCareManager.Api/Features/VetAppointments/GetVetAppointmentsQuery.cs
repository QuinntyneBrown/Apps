using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.VetAppointments;

public record GetVetAppointmentsQuery : IRequest<IEnumerable<VetAppointmentDto>>
{
    public Guid? PetId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetVetAppointmentsQueryHandler : IRequestHandler<GetVetAppointmentsQuery, IEnumerable<VetAppointmentDto>>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<GetVetAppointmentsQueryHandler> _logger;

    public GetVetAppointmentsQueryHandler(
        IPetCareManagerContext context,
        ILogger<GetVetAppointmentsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<VetAppointmentDto>> Handle(GetVetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vet appointments for pet {PetId}", request.PetId);

        var query = _context.VetAppointments.AsNoTracking();

        if (request.PetId.HasValue)
        {
            query = query.Where(a => a.PetId == request.PetId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(a => a.AppointmentDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(a => a.AppointmentDate <= request.EndDate.Value);
        }

        var appointments = await query
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync(cancellationToken);

        return appointments.Select(a => a.ToDto());
    }
}
