using GiftPlanning.Core;
using GiftPlanning.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace GiftPlanning.Api.Features;

public record CreateGiftPlanCommand(
    Guid UserId,
    Guid TenantId,
    Guid? CelebrationId,
    string RecipientName,
    string GiftIdea,
    decimal Budget,
    string? Notes) : IRequest<GiftPlanDto>;

public class CreateGiftPlanCommandHandler : IRequestHandler<CreateGiftPlanCommand, GiftPlanDto>
{
    private readonly IGiftPlanningDbContext _context;
    private readonly ILogger<CreateGiftPlanCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateGiftPlanCommandHandler(
        IGiftPlanningDbContext context,
        ILogger<CreateGiftPlanCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<GiftPlanDto> Handle(CreateGiftPlanCommand request, CancellationToken cancellationToken)
    {
        var giftPlan = new GiftPlan
        {
            GiftPlanId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            CelebrationId = request.CelebrationId,
            RecipientName = request.RecipientName,
            GiftIdea = request.GiftIdea,
            Budget = request.Budget,
            Notes = request.Notes,
            IsPurchased = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.GiftPlans.Add(giftPlan);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishGiftPlanCreatedEventAsync(giftPlan);

        _logger.LogInformation("Gift plan created: {GiftPlanId}", giftPlan.GiftPlanId);

        return giftPlan.ToDto();
    }

    private Task PublishGiftPlanCreatedEventAsync(GiftPlan giftPlan)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("giftplanning-events", ExchangeType.Topic, durable: true);

            var @event = new GiftPlanCreatedEvent
            {
                UserId = giftPlan.UserId,
                TenantId = giftPlan.TenantId,
                GiftPlanId = giftPlan.GiftPlanId,
                CelebrationId = giftPlan.CelebrationId ?? Guid.Empty,
                GiftIdea = giftPlan.GiftIdea,
                Budget = giftPlan.Budget
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("giftplanning-events", "giftplan.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish GiftPlanCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
