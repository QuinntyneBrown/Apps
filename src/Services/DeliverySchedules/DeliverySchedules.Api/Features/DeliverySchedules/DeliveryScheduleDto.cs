namespace DeliverySchedules.Api.Features.DeliverySchedules;

public record DeliveryScheduleDto
{
    public Guid DeliveryScheduleId { get; init; }
    public Guid LetterId { get; init; }
    public DateTime ScheduledDateTime { get; init; }
    public string DeliveryMethod { get; init; } = string.Empty;
    public string? RecipientContact { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
}
