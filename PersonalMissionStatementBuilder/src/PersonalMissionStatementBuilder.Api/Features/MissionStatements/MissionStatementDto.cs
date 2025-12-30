using PersonalMissionStatementBuilder.Core;

namespace PersonalMissionStatementBuilder.Api.Features.MissionStatements;

public record MissionStatementDto
{
    public Guid MissionStatementId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Text { get; init; } = string.Empty;
    public int Version { get; init; }
    public bool IsCurrentVersion { get; init; }
    public DateTime StatementDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class MissionStatementExtensions
{
    public static MissionStatementDto ToDto(this MissionStatement missionStatement)
    {
        return new MissionStatementDto
        {
            MissionStatementId = missionStatement.MissionStatementId,
            UserId = missionStatement.UserId,
            Title = missionStatement.Title,
            Text = missionStatement.Text,
            Version = missionStatement.Version,
            IsCurrentVersion = missionStatement.IsCurrentVersion,
            StatementDate = missionStatement.StatementDate,
            CreatedAt = missionStatement.CreatedAt,
            UpdatedAt = missionStatement.UpdatedAt,
        };
    }
}
