using Lessons.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lessons.Api.Features;

public record DeleteLessonCommand(Guid LessonId) : IRequest<bool>;

public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, bool>
{
    private readonly ILessonsDbContext _context;
    private readonly ILogger<DeleteLessonCommandHandler> _logger;

    public DeleteLessonCommandHandler(ILessonsDbContext context, ILogger<DeleteLessonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken);

        if (lesson == null) return false;

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Lesson deleted: {LessonId}", request.LessonId);

        return true;
    }
}
