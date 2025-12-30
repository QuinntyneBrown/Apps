using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Lesson;

public record GetLessonByIdQuery : IRequest<LessonDto?>
{
    public Guid LessonId { get; init; }
}

public class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, LessonDto?>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<GetLessonByIdQueryHandler> _logger;

    public GetLessonByIdQueryHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<GetLessonByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LessonDto?> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting lesson {LessonId}", request.LessonId);

        var lesson = await _context.Lessons
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken);

        if (lesson == null)
        {
            _logger.LogWarning("Lesson {LessonId} not found", request.LessonId);
            return null;
        }

        return lesson.ToDto();
    }
}
