using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.VetAppointments;

public record GetVetAppointmentByIdQuery : IRequest<VetAppointmentDto?>
{
    public Guid VetAppointmentId { get; init; }
}

public class GetVetAppointmentByIdQueryHandler : IRequestHandler<GetVetAppointmentByIdQuery, VetAppointmentDto?>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<GetVetAppointmentByIdQueryHandler> _logger;

    public GetVetAppointmentByIdQueryHandler(
        IPetCareManagerContext context,
        ILogger<GetVetAppointmentByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VetAppointmentDto?> Handle(GetVetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vet appointment {VetAppointmentId}", request.VetAppointmentId);

        var appointment = await _context.VetAppointments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.VetAppointmentId == request.VetAppointmentId, cancellationToken);

        return appointment?.ToDto();
    }
}
