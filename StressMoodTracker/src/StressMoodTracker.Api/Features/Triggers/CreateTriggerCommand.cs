using StressMoodTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Triggers;

public record CreateTriggerCommand : IRequest<TriggerDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string TriggerType { get; init; } = string.Empty;
    public int ImpactLevel { get; init; }
}

public class CreateTriggerCommandHandler : IRequestHandler<CreateTriggerCommand, TriggerDto>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<CreateTriggerCommandHandler> _logger;

    public CreateTriggerCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<CreateTriggerCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TriggerDto> Handle(CreateTriggerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating trigger for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var trigger = new Trigger
        {
            TriggerId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            TriggerType = request.TriggerType,
            ImpactLevel = request.ImpactLevel,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Triggers.Add(trigger);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created trigger {TriggerId} for user {UserId}",
            trigger.TriggerId,
            request.UserId);

        return trigger.ToDto();
    }
}
