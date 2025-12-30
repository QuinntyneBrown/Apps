using PersonalHealthDashboard.Core;

namespace PersonalHealthDashboard.Api.Features.WearableData;

public record WearableDataDto
{
    public Guid WearableDataId { get; init; }
    public Guid UserId { get; init; }
    public string DeviceName { get; init; } = string.Empty;
    public string DataType { get; init; } = string.Empty;
    public double Value { get; init; }
    public string Unit { get; init; } = string.Empty;
    public DateTime RecordedAt { get; init; }
    public DateTime SyncedAt { get; init; }
    public string? Metadata { get; init; }
    public DateTime CreatedAt { get; init; }
    public double DataAgeInHours { get; init; }
}

public static class WearableDataExtensions
{
    public static WearableDataDto ToDto(this Core.WearableData wearableData)
    {
        return new WearableDataDto
        {
            WearableDataId = wearableData.WearableDataId,
            UserId = wearableData.UserId,
            DeviceName = wearableData.DeviceName,
            DataType = wearableData.DataType,
            Value = wearableData.Value,
            Unit = wearableData.Unit,
            RecordedAt = wearableData.RecordedAt,
            SyncedAt = wearableData.SyncedAt,
            Metadata = wearableData.Metadata,
            CreatedAt = wearableData.CreatedAt,
            DataAgeInHours = wearableData.GetDataAgeInHours(),
        };
    }
}
