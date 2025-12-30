using PersonalMissionStatementBuilder.Core;

namespace PersonalMissionStatementBuilder.Api.Features.Values;

public record ValueDto
{
    public Guid ValueId { get; init; }
    public Guid? MissionStatementId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int Priority { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ValueExtensions
{
    public static ValueDto ToDto(this Value value)
    {
        return new ValueDto
        {
            ValueId = value.ValueId,
            MissionStatementId = value.MissionStatementId,
            UserId = value.UserId,
            Name = value.Name,
            Description = value.Description,
            Priority = value.Priority,
            CreatedAt = value.CreatedAt,
            UpdatedAt = value.UpdatedAt,
        };
    }
}
