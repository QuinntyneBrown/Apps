using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.LessonReminder;

public record UpdateLessonReminderCommand : IRequest<LessonReminderDto?>
{
    public Guid LessonReminderId { get; init; }
    public DateTime ReminderDateTime { get; init; }
    public string? Message { get; init; }
    public bool IsSent { get; init; }
    public bool IsDismissed { get; init; }
}

public class UpdateLessonReminderCommandHandler : IRequestHandler<UpdateLessonReminderCommand, LessonReminderDto?>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<UpdateLessonReminderCommandHandler> _logger;

    public UpdateLessonReminderCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<UpdateLessonReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LessonReminderDto?> Handle(UpdateLessonReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reminder {LessonReminderId}", request.LessonReminderId);

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.LessonReminderId == request.LessonReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {LessonReminderId} not found", request.LessonReminderId);
            return null;
        }

        reminder.ReminderDateTime = request.ReminderDateTime;
        reminder.Message = request.Message;
        reminder.IsSent = request.IsSent;
        reminder.IsDismissed = request.IsDismissed;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated reminder {LessonReminderId}", request.LessonReminderId);

        return reminder.ToDto();
    }
}
