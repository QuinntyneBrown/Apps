using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.LessonReminder;

public record GetLessonRemindersQuery : IRequest<IEnumerable<LessonReminderDto>>
{
    public Guid? UserId { get; init; }
    public Guid? LessonId { get; init; }
    public bool? IsSent { get; init; }
    public bool? IsDismissed { get; init; }
}

public class GetLessonRemindersQueryHandler : IRequestHandler<GetLessonRemindersQuery, IEnumerable<LessonReminderDto>>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<GetLessonRemindersQueryHandler> _logger;

    public GetLessonRemindersQueryHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<GetLessonRemindersQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<LessonReminderDto>> Handle(GetLessonRemindersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reminders for user {UserId}", request.UserId);

        var query = _context.Reminders.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.LessonId.HasValue)
        {
            query = query.Where(r => r.LessonId == request.LessonId.Value);
        }

        if (request.IsSent.HasValue)
        {
            query = query.Where(r => r.IsSent == request.IsSent.Value);
        }

        if (request.IsDismissed.HasValue)
        {
            query = query.Where(r => r.IsDismissed == request.IsDismissed.Value);
        }

        var reminders = await query
            .OrderBy(r => r.ReminderDateTime)
            .ToListAsync(cancellationToken);

        return reminders.Select(r => r.ToDto());
    }
}
