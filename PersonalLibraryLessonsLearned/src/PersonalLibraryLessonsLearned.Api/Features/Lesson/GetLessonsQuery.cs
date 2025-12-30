using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Lesson;

public record GetLessonsQuery : IRequest<IEnumerable<LessonDto>>
{
    public Guid? UserId { get; init; }
    public Guid? SourceId { get; init; }
    public LessonCategory? Category { get; init; }
    public bool? IsApplied { get; init; }
}

public class GetLessonsQueryHandler : IRequestHandler<GetLessonsQuery, IEnumerable<LessonDto>>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<GetLessonsQueryHandler> _logger;

    public GetLessonsQueryHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<GetLessonsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<LessonDto>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting lessons for user {UserId}", request.UserId);

        var query = _context.Lessons.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(l => l.UserId == request.UserId.Value);
        }

        if (request.SourceId.HasValue)
        {
            query = query.Where(l => l.SourceId == request.SourceId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(l => l.Category == request.Category.Value);
        }

        if (request.IsApplied.HasValue)
        {
            query = query.Where(l => l.IsApplied == request.IsApplied.Value);
        }

        var lessons = await query
            .OrderByDescending(l => l.DateLearned)
            .ToListAsync(cancellationToken);

        return lessons.Select(l => l.ToDto());
    }
}
