using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Lesson;

public record CreateLessonCommand : IRequest<LessonDto>
{
    public Guid UserId { get; init; }
    public Guid? SourceId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public LessonCategory Category { get; init; }
    public string? Tags { get; init; }
    public DateTime DateLearned { get; init; }
    public string? Application { get; init; }
}

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, LessonDto>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<CreateLessonCommandHandler> _logger;

    public CreateLessonCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<CreateLessonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LessonDto> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating lesson for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var lesson = new Core.Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = request.UserId,
            SourceId = request.SourceId,
            Title = request.Title,
            Content = request.Content,
            Category = request.Category,
            Tags = request.Tags,
            DateLearned = request.DateLearned,
            Application = request.Application,
            IsApplied = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created lesson {LessonId} for user {UserId}",
            lesson.LessonId,
            request.UserId);

        return lesson.ToDto();
    }
}
