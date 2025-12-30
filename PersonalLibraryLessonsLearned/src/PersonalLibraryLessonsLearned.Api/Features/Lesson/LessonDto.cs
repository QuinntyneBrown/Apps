using PersonalLibraryLessonsLearned.Core;

namespace PersonalLibraryLessonsLearned.Api.Features.Lesson;

public record LessonDto
{
    public Guid LessonId { get; init; }
    public Guid UserId { get; init; }
    public Guid? SourceId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public LessonCategory Category { get; init; }
    public string? Tags { get; init; }
    public DateTime DateLearned { get; init; }
    public string? Application { get; init; }
    public bool IsApplied { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class LessonExtensions
{
    public static LessonDto ToDto(this Core.Lesson lesson)
    {
        return new LessonDto
        {
            LessonId = lesson.LessonId,
            UserId = lesson.UserId,
            SourceId = lesson.SourceId,
            Title = lesson.Title,
            Content = lesson.Content,
            Category = lesson.Category,
            Tags = lesson.Tags,
            DateLearned = lesson.DateLearned,
            Application = lesson.Application,
            IsApplied = lesson.IsApplied,
            CreatedAt = lesson.CreatedAt,
        };
    }
}
