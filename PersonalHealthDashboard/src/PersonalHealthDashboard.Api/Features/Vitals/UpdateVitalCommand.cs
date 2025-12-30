using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.Vitals;

public record UpdateVitalCommand : IRequest<VitalDto?>
{
    public Guid VitalId { get; init; }
    public VitalType VitalType { get; init; }
    public double Value { get; init; }
    public string Unit { get; init; } = string.Empty;
    public DateTime MeasuredAt { get; init; }
    public string? Notes { get; init; }
    public string? Source { get; init; }
}

public class UpdateVitalCommandHandler : IRequestHandler<UpdateVitalCommand, VitalDto?>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<UpdateVitalCommandHandler> _logger;

    public UpdateVitalCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<UpdateVitalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VitalDto?> Handle(UpdateVitalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating vital {VitalId}", request.VitalId);

        var vital = await _context.Vitals
            .FirstOrDefaultAsync(v => v.VitalId == request.VitalId, cancellationToken);

        if (vital == null)
        {
            _logger.LogWarning("Vital {VitalId} not found", request.VitalId);
            return null;
        }

        vital.VitalType = request.VitalType;
        vital.Value = request.Value;
        vital.Unit = request.Unit;
        vital.MeasuredAt = request.MeasuredAt;
        vital.Notes = request.Notes;
        vital.Source = request.Source;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated vital {VitalId}", request.VitalId);

        return vital.ToDto();
    }
}
