using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.Vitals;

public record GetVitalByIdQuery : IRequest<VitalDto?>
{
    public Guid VitalId { get; init; }
}

public class GetVitalByIdQueryHandler : IRequestHandler<GetVitalByIdQuery, VitalDto?>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<GetVitalByIdQueryHandler> _logger;

    public GetVitalByIdQueryHandler(
        IPersonalHealthDashboardContext context,
        ILogger<GetVitalByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VitalDto?> Handle(GetVitalByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vital {VitalId}", request.VitalId);

        var vital = await _context.Vitals
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VitalId == request.VitalId, cancellationToken);

        if (vital == null)
        {
            _logger.LogWarning("Vital {VitalId} not found", request.VitalId);
            return null;
        }

        return vital.ToDto();
    }
}
