using Activities.Core;
using Activities.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Activities.Api.Features;

public record CreateActivityCommand(
    Guid UserId,
    Guid TenantId,
    string Name,
    ActivityType Type,
    string? Description,
    string? Location,
    string? CoachName,
    string? ContactInfo,
    decimal? Cost) : IRequest<ActivityDto>;

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ActivityDto>
{
    private readonly IActivitiesDbContext _context;
    private readonly ILogger<CreateActivityCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateActivityCommandHandler(
        IActivitiesDbContext context,
        ILogger<CreateActivityCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ActivityDto> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = new Activity
        {
            ActivityId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            Type = request.Type,
            Description = request.Description,
            Location = request.Location,
            CoachName = request.CoachName,
            ContactInfo = request.ContactInfo,
            Cost = request.Cost,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Activities.Add(activity);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishActivityCreatedEventAsync(activity);

        _logger.LogInformation("Activity created: {ActivityId}", activity.ActivityId);

        return activity.ToDto();
    }

    private Task PublishActivityCreatedEventAsync(Activity activity)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("activities-events", ExchangeType.Topic, durable: true);

            var @event = new ActivityCreatedEvent
            {
                UserId = activity.UserId,
                TenantId = activity.TenantId,
                ActivityId = activity.ActivityId,
                Name = activity.Name,
                ActivityType = activity.Type.ToString()
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("activities-events", "activity.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ActivityCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
