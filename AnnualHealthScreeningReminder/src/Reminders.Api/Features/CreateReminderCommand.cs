using Reminders.Core;
using Reminders.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Reminders.Api.Features;

public record CreateReminderCommand(
    Guid UserId,
    Guid TenantId,
    Guid? CelebrationId,
    string Title,
    string Message,
    DateTime ScheduledDate,
    bool IsActive) : IRequest<ReminderDto>;

public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, ReminderDto>
{
    private readonly IRemindersDbContext _context;
    private readonly ILogger<CreateReminderCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateReminderCommandHandler(
        IRemindersDbContext context,
        ILogger<CreateReminderCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ReminderDto> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
    {
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            CelebrationId = request.CelebrationId,
            Title = request.Title,
            Message = request.Message,
            ScheduledDate = request.ScheduledDate,
            IsActive = request.IsActive,
            IsTriggered = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Reminder created: {ReminderId}", reminder.ReminderId);

        return reminder.ToDto();
    }
}
