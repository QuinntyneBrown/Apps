using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.Vitals;

public record CreateVitalCommand : IRequest<VitalDto>
{
    public Guid UserId { get; init; }
    public VitalType VitalType { get; init; }
    public double Value { get; init; }
    public string Unit { get; init; } = string.Empty;
    public DateTime MeasuredAt { get; init; }
    public string? Notes { get; init; }
    public string? Source { get; init; }
}

public class CreateVitalCommandHandler : IRequestHandler<CreateVitalCommand, VitalDto>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<CreateVitalCommandHandler> _logger;

    public CreateVitalCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<CreateVitalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VitalDto> Handle(CreateVitalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating vital for user {UserId}, type: {VitalType}",
            request.UserId,
            request.VitalType);

        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = request.UserId,
            VitalType = request.VitalType,
            Value = request.Value,
            Unit = request.Unit,
            MeasuredAt = request.MeasuredAt,
            Notes = request.Notes,
            Source = request.Source,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Vitals.Add(vital);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created vital {VitalId} for user {UserId}",
            vital.VitalId,
            request.UserId);

        return vital.ToDto();
    }
}
