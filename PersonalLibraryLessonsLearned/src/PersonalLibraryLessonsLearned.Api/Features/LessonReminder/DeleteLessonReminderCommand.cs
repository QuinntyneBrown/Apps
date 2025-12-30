using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.LessonReminder;

public record DeleteLessonReminderCommand : IRequest<bool>
{
    public Guid LessonReminderId { get; init; }
}

public class DeleteLessonReminderCommandHandler : IRequestHandler<DeleteLessonReminderCommand, bool>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<DeleteLessonReminderCommandHandler> _logger;

    public DeleteLessonReminderCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<DeleteLessonReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteLessonReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reminder {LessonReminderId}", request.LessonReminderId);

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.LessonReminderId == request.LessonReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {LessonReminderId} not found", request.LessonReminderId);
            return false;
        }

        _context.Reminders.Remove(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted reminder {LessonReminderId}", request.LessonReminderId);

        return true;
    }
}
