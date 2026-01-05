using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Models.ReminderAggregate;
using FamilyCalendarEventPlanner.Core.Models.ReminderAggregate.Enums;
using FamilyCalendarEventPlanner.Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Reminders;

public record CreateReminderCommand : IRequest<EventReminderDto>
{
    public Guid EventId { get; init; }
    public Guid RecipientId { get; init; }
    public DateTime ReminderTime { get; init; }
    public NotificationChannel DeliveryChannel { get; init; }
}

public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, EventReminderDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ITenantContext _tenantContext;
    private readonly ILogger<CreateReminderCommandHandler> _logger;

    public CreateReminderCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ITenantContext tenantContext,
        ILogger<CreateReminderCommandHandler> logger)
    {
        _context = context;
        _tenantContext = tenantContext;
        _logger = logger;
    }

    public async Task<EventReminderDto> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating reminder for event {EventId}, recipient {RecipientId}",
            request.EventId,
            request.RecipientId);

        var reminder = new EventReminder(
            _tenantContext.TenantId,
            request.EventId,
            request.RecipientId,
            request.ReminderTime,
            request.DeliveryChannel);

        _context.EventReminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created reminder {ReminderId} for event {EventId}",
            reminder.ReminderId,
            request.EventId);

        return reminder.ToDto();
    }
}
