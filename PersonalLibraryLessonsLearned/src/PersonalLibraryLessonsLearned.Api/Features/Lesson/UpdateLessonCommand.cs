using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Lesson;

public record UpdateLessonCommand : IRequest<LessonDto?>
{
    public Guid LessonId { get; init; }
    public Guid? SourceId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public LessonCategory Category { get; init; }
    public string? Tags { get; init; }
    public DateTime DateLearned { get; init; }
    public string? Application { get; init; }
    public bool IsApplied { get; init; }
}

public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, LessonDto?>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<UpdateLessonCommandHandler> _logger;

    public UpdateLessonCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<UpdateLessonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LessonDto?> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating lesson {LessonId}", request.LessonId);

        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken);

        if (lesson == null)
        {
            _logger.LogWarning("Lesson {LessonId} not found", request.LessonId);
            return null;
        }

        lesson.SourceId = request.SourceId;
        lesson.Title = request.Title;
        lesson.Content = request.Content;
        lesson.Category = request.Category;
        lesson.Tags = request.Tags;
        lesson.DateLearned = request.DateLearned;
        lesson.Application = request.Application;
        lesson.IsApplied = request.IsApplied;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated lesson {LessonId}", request.LessonId);

        return lesson.ToDto();
    }
}
