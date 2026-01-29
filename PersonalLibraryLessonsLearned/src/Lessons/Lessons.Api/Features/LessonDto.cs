using Lessons.Core.Models;

namespace Lessons.Api.Features;

public record LessonDto(
    Guid LessonId,
    Guid UserId,
    Guid? SourceId,
    string Title,
    string Content,
    string Category,
    string? Tags,
    DateTime DateLearned,
    string? Application,
    bool IsApplied,
    DateTime CreatedAt);

public static class LessonExtensions
{
    public static LessonDto ToDto(this Lesson lesson)
    {
        return new LessonDto(
            lesson.LessonId,
            lesson.UserId,
            lesson.SourceId,
            lesson.Title,
            lesson.Content,
            lesson.Category,
            lesson.Tags,
            lesson.DateLearned,
            lesson.Application,
            lesson.IsApplied,
            lesson.CreatedAt);
    }
}
