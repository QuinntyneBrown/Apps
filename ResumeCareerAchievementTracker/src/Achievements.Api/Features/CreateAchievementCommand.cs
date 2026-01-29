using Achievements.Core;
using Achievements.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Achievements.Api.Features;

public record CreateAchievementCommand(
    Guid UserId,
    Guid TenantId,
    string Title,
    string Description,
    AchievementType AchievementType,
    DateTime AchievedDate,
    string? Organization,
    string? Impact,
    bool IsFeatured) : IRequest<AchievementDto>;

public class CreateAchievementCommandHandler : IRequestHandler<CreateAchievementCommand, AchievementDto>
{
    private readonly IAchievementsDbContext _context;
    private readonly ILogger<CreateAchievementCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateAchievementCommandHandler(
        IAchievementsDbContext context,
        ILogger<CreateAchievementCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<AchievementDto> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
    {
        var achievement = new Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Title = request.Title,
            Description = request.Description,
            AchievementType = request.AchievementType,
            AchievedDate = request.AchievedDate,
            Organization = request.Organization,
            Impact = request.Impact,
            IsFeatured = request.IsFeatured,
            CreatedAt = DateTime.UtcNow
        };

        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishAchievementCreatedEventAsync(achievement);

        _logger.LogInformation("Achievement created: {AchievementId}", achievement.AchievementId);

        return achievement.ToDto();
    }

    private Task PublishAchievementCreatedEventAsync(Achievement achievement)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("achievements-events", ExchangeType.Topic, durable: true);

            var @event = new AchievementCreatedEvent
            {
                UserId = achievement.UserId,
                TenantId = achievement.TenantId,
                AchievementId = achievement.AchievementId,
                Title = achievement.Title,
                AchievementType = achievement.AchievementType.ToString(),
                AchievedDate = achievement.AchievedDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("achievements-events", "achievement.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish AchievementCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
