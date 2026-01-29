using Lessons.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lessons.Api.Features;

public record UpdateLessonCommand(
    Guid LessonId,
    string Title,
    string Content,
    string Category,
    string? Tags,
    string? Application,
    bool IsApplied) : IRequest<LessonDto?>;

public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, LessonDto?>
{
    private readonly ILessonsDbContext _context;
    private readonly ILogger<UpdateLessonCommandHandler> _logger;

    public UpdateLessonCommandHandler(ILessonsDbContext context, ILogger<UpdateLessonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LessonDto?> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken);

        if (lesson == null) return null;

        lesson.Title = request.Title;
        lesson.Content = request.Content;
        lesson.Category = request.Category;
        lesson.Tags = request.Tags;
        lesson.Application = request.Application;
        lesson.IsApplied = request.IsApplied;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Lesson updated: {LessonId}", lesson.LessonId);

        return lesson.ToDto();
    }
}
