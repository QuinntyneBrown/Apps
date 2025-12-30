using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Triggers;

public record UpdateTriggerCommand : IRequest<TriggerDto?>
{
    public Guid TriggerId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string TriggerType { get; init; } = string.Empty;
    public int ImpactLevel { get; init; }
}

public class UpdateTriggerCommandHandler : IRequestHandler<UpdateTriggerCommand, TriggerDto?>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<UpdateTriggerCommandHandler> _logger;

    public UpdateTriggerCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<UpdateTriggerCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TriggerDto?> Handle(UpdateTriggerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating trigger {TriggerId}", request.TriggerId);

        var trigger = await _context.Triggers
            .FirstOrDefaultAsync(t => t.TriggerId == request.TriggerId, cancellationToken);

        if (trigger == null)
        {
            _logger.LogWarning("Trigger {TriggerId} not found", request.TriggerId);
            return null;
        }

        trigger.Name = request.Name;
        trigger.Description = request.Description;
        trigger.TriggerType = request.TriggerType;
        trigger.ImpactLevel = request.ImpactLevel;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated trigger {TriggerId}", request.TriggerId);

        return trigger.ToDto();
    }
}
