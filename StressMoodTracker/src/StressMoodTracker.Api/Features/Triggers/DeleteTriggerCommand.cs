using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Triggers;

public record DeleteTriggerCommand : IRequest<bool>
{
    public Guid TriggerId { get; init; }
}

public class DeleteTriggerCommandHandler : IRequestHandler<DeleteTriggerCommand, bool>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<DeleteTriggerCommandHandler> _logger;

    public DeleteTriggerCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<DeleteTriggerCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTriggerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting trigger {TriggerId}", request.TriggerId);

        var trigger = await _context.Triggers
            .FirstOrDefaultAsync(t => t.TriggerId == request.TriggerId, cancellationToken);

        if (trigger == null)
        {
            _logger.LogWarning("Trigger {TriggerId} not found", request.TriggerId);
            return false;
        }

        _context.Triggers.Remove(trigger);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted trigger {TriggerId}", request.TriggerId);

        return true;
    }
}
