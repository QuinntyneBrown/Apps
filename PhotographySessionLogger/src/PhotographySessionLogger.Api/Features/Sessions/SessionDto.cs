using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Api.Features.Sessions;

public record SessionDto
{
    public Guid SessionId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public SessionType SessionType { get; init; }
    public DateTime SessionDate { get; init; }
    public string? Location { get; init; }
    public string? Client { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public int PhotoCount { get; init; }
}

public static class SessionExtensions
{
    public static SessionDto ToDto(this Session session)
    {
        return new SessionDto
        {
            SessionId = session.SessionId,
            UserId = session.UserId,
            Title = session.Title,
            SessionType = session.SessionType,
            SessionDate = session.SessionDate,
            Location = session.Location,
            Client = session.Client,
            Notes = session.Notes,
            CreatedAt = session.CreatedAt,
            PhotoCount = session.Photos?.Count ?? 0,
        };
    }
}
