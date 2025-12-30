using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Api.Features.Photos;

public record PhotoDto
{
    public Guid PhotoId { get; init; }
    public Guid UserId { get; init; }
    public Guid SessionId { get; init; }
    public string FileName { get; init; } = string.Empty;
    public string? FilePath { get; init; }
    public string? CameraSettings { get; init; }
    public int? Rating { get; init; }
    public string? Tags { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class PhotoExtensions
{
    public static PhotoDto ToDto(this Photo photo)
    {
        return new PhotoDto
        {
            PhotoId = photo.PhotoId,
            UserId = photo.UserId,
            SessionId = photo.SessionId,
            FileName = photo.FileName,
            FilePath = photo.FilePath,
            CameraSettings = photo.CameraSettings,
            Rating = photo.Rating,
            Tags = photo.Tags,
            CreatedAt = photo.CreatedAt,
        };
    }
}
