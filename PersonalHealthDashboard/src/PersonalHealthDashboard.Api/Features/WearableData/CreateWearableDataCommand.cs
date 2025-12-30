using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.WearableData;

public record CreateWearableDataCommand : IRequest<WearableDataDto>
{
    public Guid UserId { get; init; }
    public string DeviceName { get; init; } = string.Empty;
    public string DataType { get; init; } = string.Empty;
    public double Value { get; init; }
    public string Unit { get; init; } = string.Empty;
    public DateTime RecordedAt { get; init; }
    public DateTime SyncedAt { get; init; }
    public string? Metadata { get; init; }
}

public class CreateWearableDataCommandHandler : IRequestHandler<CreateWearableDataCommand, WearableDataDto>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<CreateWearableDataCommandHandler> _logger;

    public CreateWearableDataCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<CreateWearableDataCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WearableDataDto> Handle(CreateWearableDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating wearable data for user {UserId}, device: {DeviceName}, type: {DataType}",
            request.UserId,
            request.DeviceName,
            request.DataType);

        var wearableData = new Core.WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = request.UserId,
            DeviceName = request.DeviceName,
            DataType = request.DataType,
            Value = request.Value,
            Unit = request.Unit,
            RecordedAt = request.RecordedAt,
            SyncedAt = request.SyncedAt,
            Metadata = request.Metadata,
            CreatedAt = DateTime.UtcNow,
        };

        _context.WearableData.Add(wearableData);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created wearable data {WearableDataId} for user {UserId}",
            wearableData.WearableDataId,
            request.UserId);

        return wearableData.ToDto();
    }
}
