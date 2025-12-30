using PersonalHealthDashboard.Core;

namespace PersonalHealthDashboard.Api.Features.Vitals;

public record VitalDto
{
    public Guid VitalId { get; init; }
    public Guid UserId { get; init; }
    public VitalType VitalType { get; init; }
    public double Value { get; init; }
    public string Unit { get; init; } = string.Empty;
    public DateTime MeasuredAt { get; init; }
    public string? Notes { get; init; }
    public string? Source { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class VitalExtensions
{
    public static VitalDto ToDto(this Vital vital)
    {
        return new VitalDto
        {
            VitalId = vital.VitalId,
            UserId = vital.UserId,
            VitalType = vital.VitalType,
            Value = vital.Value,
            Unit = vital.Unit,
            MeasuredAt = vital.MeasuredAt,
            Notes = vital.Notes,
            Source = vital.Source,
            CreatedAt = vital.CreatedAt,
        };
    }
}
