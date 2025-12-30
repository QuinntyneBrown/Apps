using ProfessionalReadingList.Core;

namespace ProfessionalReadingList.Api.Features.ReadingProgress;

public record ReadingProgressDto
{
    public Guid ReadingProgressId { get; init; }
    public Guid UserId { get; init; }
    public Guid ResourceId { get; init; }
    public string Status { get; init; } = "Not Started";
    public int? CurrentPage { get; init; }
    public int ProgressPercentage { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? CompletionDate { get; init; }
    public int? Rating { get; init; }
    public string? Review { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ReadingProgressExtensions
{
    public static ReadingProgressDto ToDto(this Core.ReadingProgress readingProgress)
    {
        return new ReadingProgressDto
        {
            ReadingProgressId = readingProgress.ReadingProgressId,
            UserId = readingProgress.UserId,
            ResourceId = readingProgress.ResourceId,
            Status = readingProgress.Status,
            CurrentPage = readingProgress.CurrentPage,
            ProgressPercentage = readingProgress.ProgressPercentage,
            StartDate = readingProgress.StartDate,
            CompletionDate = readingProgress.CompletionDate,
            Rating = readingProgress.Rating,
            Review = readingProgress.Review,
            CreatedAt = readingProgress.CreatedAt,
            UpdatedAt = readingProgress.UpdatedAt,
        };
    }
}
