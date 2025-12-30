using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.LessonReminder;

public record CreateLessonReminderCommand : IRequest<LessonReminderDto>
{
    public Guid LessonId { get; init; }
    public Guid UserId { get; init; }
    public DateTime ReminderDateTime { get; init; }
    public string? Message { get; init; }
}

public class CreateLessonReminderCommandHandler : IRequestHandler<CreateLessonReminderCommand, LessonReminderDto>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<CreateLessonReminderCommandHandler> _logger;

    public CreateLessonReminderCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<CreateLessonReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LessonReminderDto> Handle(CreateLessonReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating reminder for lesson {LessonId}, user {UserId}",
            request.LessonId,
            request.UserId);

        var reminder = new Core.LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = request.LessonId,
            UserId = request.UserId,
            ReminderDateTime = request.ReminderDateTime,
            Message = request.Message,
            IsSent = false,
            IsDismissed = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created reminder {LessonReminderId} for lesson {LessonId}",
            reminder.LessonReminderId,
            request.LessonId);

        return reminder.ToDto();
    }
}
