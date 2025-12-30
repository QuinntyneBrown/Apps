using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.WearableData;

public record UpdateWearableDataCommand : IRequest<WearableDataDto?>
{
    public Guid WearableDataId { get; init; }
    public string DeviceName { get; init; } = string.Empty;
    public string DataType { get; init; } = string.Empty;
    public double Value { get; init; }
    public string Unit { get; init; } = string.Empty;
    public DateTime RecordedAt { get; init; }
    public DateTime SyncedAt { get; init; }
    public string? Metadata { get; init; }
}

public class UpdateWearableDataCommandHandler : IRequestHandler<UpdateWearableDataCommand, WearableDataDto?>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<UpdateWearableDataCommandHandler> _logger;

    public UpdateWearableDataCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<UpdateWearableDataCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WearableDataDto?> Handle(UpdateWearableDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating wearable data {WearableDataId}", request.WearableDataId);

        var wearableData = await _context.WearableData
            .FirstOrDefaultAsync(w => w.WearableDataId == request.WearableDataId, cancellationToken);

        if (wearableData == null)
        {
            _logger.LogWarning("Wearable data {WearableDataId} not found", request.WearableDataId);
            return null;
        }

        wearableData.DeviceName = request.DeviceName;
        wearableData.DataType = request.DataType;
        wearableData.Value = request.Value;
        wearableData.Unit = request.Unit;
        wearableData.RecordedAt = request.RecordedAt;
        wearableData.SyncedAt = request.SyncedAt;
        wearableData.Metadata = request.Metadata;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated wearable data {WearableDataId}", request.WearableDataId);

        return wearableData.ToDto();
    }
}
