using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.LessonReminder;

public record GetLessonReminderByIdQuery : IRequest<LessonReminderDto?>
{
    public Guid LessonReminderId { get; init; }
}

public class GetLessonReminderByIdQueryHandler : IRequestHandler<GetLessonReminderByIdQuery, LessonReminderDto?>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<GetLessonReminderByIdQueryHandler> _logger;

    public GetLessonReminderByIdQueryHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<GetLessonReminderByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LessonReminderDto?> Handle(GetLessonReminderByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reminder {LessonReminderId}", request.LessonReminderId);

        var reminder = await _context.Reminders
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.LessonReminderId == request.LessonReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {LessonReminderId} not found", request.LessonReminderId);
            return null;
        }

        return reminder.ToDto();
    }
}
