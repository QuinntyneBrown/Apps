using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Patterns;

public record DeletePatternCommand : IRequest<bool>
{
    public Guid PatternId { get; init; }
}

public class DeletePatternCommandHandler : IRequestHandler<DeletePatternCommand, bool>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<DeletePatternCommandHandler> _logger;

    public DeletePatternCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<DeletePatternCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePatternCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting pattern {PatternId}", request.PatternId);

        var pattern = await _context.Patterns
            .FirstOrDefaultAsync(p => p.PatternId == request.PatternId, cancellationToken);

        if (pattern == null)
        {
            _logger.LogWarning("Pattern {PatternId} not found", request.PatternId);
            return false;
        }

        _context.Patterns.Remove(pattern);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted pattern {PatternId}", request.PatternId);

        return true;
    }
}
