using Manuals.Core.Models;

namespace Manuals.Api.Features;

public record ManualDto
{
    public Guid ManualId { get; init; }
    public Guid ApplianceId { get; init; }
    public string? Title { get; init; }
    public string? FileUrl { get; init; }
    public string? FileType { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ManualExtensions
{
    public static ManualDto ToDto(this Manual manual) => new()
    {
        ManualId = manual.ManualId,
        ApplianceId = manual.ApplianceId,
        Title = manual.Title,
        FileUrl = manual.FileUrl,
        FileType = manual.FileType,
        CreatedAt = manual.CreatedAt
    };
}
