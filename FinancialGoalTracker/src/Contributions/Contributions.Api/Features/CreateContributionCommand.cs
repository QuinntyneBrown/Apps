using Contributions.Core;
using Contributions.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Contributions.Api.Features;

public record CreateContributionCommand : IRequest<ContributionDto>
{
    public Guid TenantId { get; init; }
    public Guid GoalId { get; init; }
    public decimal Amount { get; init; }
    public DateTime ContributionDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateContributionCommandHandler : IRequestHandler<CreateContributionCommand, ContributionDto>
{
    private readonly IContributionsDbContext _context;
    private readonly ILogger<CreateContributionCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateContributionCommandHandler(
        IContributionsDbContext context,
        ILogger<CreateContributionCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ContributionDto> Handle(CreateContributionCommand request, CancellationToken cancellationToken)
    {
        var contribution = new Contribution(
            request.TenantId,
            request.GoalId,
            request.Amount,
            request.ContributionDate,
            request.Notes);

        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishContributionAddedEventAsync(contribution, cancellationToken);

        _logger.LogInformation("Contribution created: {ContributionId}", contribution.ContributionId);

        return new ContributionDto
        {
            ContributionId = contribution.ContributionId,
            GoalId = contribution.GoalId,
            Amount = contribution.Amount,
            ContributionDate = contribution.ContributionDate,
            Notes = contribution.Notes,
            CreatedAt = contribution.CreatedAt
        };
    }

    private async Task PublishContributionAddedEventAsync(Contribution contribution, CancellationToken cancellationToken)
    {
        if (_rabbitConnection == null) return;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("contributions-events", ExchangeType.Topic, durable: true);

            var @event = new ContributionAddedEvent
            {
                ContributionId = contribution.ContributionId,
                GoalId = contribution.GoalId,
                Amount = contribution.Amount,
                TenantId = contribution.TenantId
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("contributions-events", "contribution.added", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ContributionAddedEvent");
        }
    }
}
