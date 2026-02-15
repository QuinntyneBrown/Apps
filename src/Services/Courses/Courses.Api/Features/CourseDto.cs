namespace Courses.Api.Features;

public record CourseDto(Guid CourseId, Guid TenantId, Guid UserId, string Title, string? Provider, string? Url, int? DurationHours, bool IsCompleted, DateTime? CompletedAt, DateTime CreatedAt);
