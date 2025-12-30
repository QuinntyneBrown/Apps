using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Lesson;

public record DeleteLessonCommand : IRequest<bool>
{
    public Guid LessonId { get; init; }
}

public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, bool>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<DeleteLessonCommandHandler> _logger;

    public DeleteLessonCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<DeleteLessonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting lesson {LessonId}", request.LessonId);

        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken);

        if (lesson == null)
        {
            _logger.LogWarning("Lesson {LessonId} not found", request.LessonId);
            return false;
        }

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted lesson {LessonId}", request.LessonId);

        return true;
    }
}
