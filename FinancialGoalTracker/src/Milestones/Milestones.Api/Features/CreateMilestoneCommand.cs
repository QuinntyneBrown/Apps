using Milestones.Core;
using Milestones.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Milestones.Api.Features;

public record CreateMilestoneCommand : IRequest<MilestoneDto>
{
    public Guid TenantId { get; init; }
    public Guid GoalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal TargetAmount { get; init; }
}

public class CreateMilestoneCommandHandler : IRequestHandler<CreateMilestoneCommand, MilestoneDto>
{
    private readonly IMilestonesDbContext _context;
    private readonly ILogger<CreateMilestoneCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateMilestoneCommandHandler(
        IMilestonesDbContext context,
        ILogger<CreateMilestoneCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<MilestoneDto> Handle(CreateMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = new Milestone(
            request.TenantId,
            request.GoalId,
            request.Name,
            request.TargetAmount);

        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Milestone created: {MilestoneId}", milestone.MilestoneId);

        return new MilestoneDto
        {
            MilestoneId = milestone.MilestoneId,
            GoalId = milestone.GoalId,
            Name = milestone.Name,
            TargetAmount = milestone.TargetAmount,
            IsReached = milestone.IsReached,
            ReachedAt = milestone.ReachedAt,
            CreatedAt = milestone.CreatedAt
        };
    }
}
