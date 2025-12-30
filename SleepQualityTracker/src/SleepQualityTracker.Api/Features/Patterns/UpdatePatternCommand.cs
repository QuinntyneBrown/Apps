using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Patterns;

public record UpdatePatternCommand : IRequest<PatternDto?>
{
    public Guid PatternId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string PatternType { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int ConfidenceLevel { get; init; }
    public string? Insights { get; init; }
}

public class UpdatePatternCommandHandler : IRequestHandler<UpdatePatternCommand, PatternDto?>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<UpdatePatternCommandHandler> _logger;

    public UpdatePatternCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<UpdatePatternCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PatternDto?> Handle(UpdatePatternCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating pattern {PatternId}", request.PatternId);

        var pattern = await _context.Patterns
            .FirstOrDefaultAsync(p => p.PatternId == request.PatternId, cancellationToken);

        if (pattern == null)
        {
            _logger.LogWarning("Pattern {PatternId} not found", request.PatternId);
            return null;
        }

        pattern.Name = request.Name;
        pattern.Description = request.Description;
        pattern.PatternType = request.PatternType;
        pattern.StartDate = request.StartDate;
        pattern.EndDate = request.EndDate;
        pattern.ConfidenceLevel = request.ConfidenceLevel;
        pattern.Insights = request.Insights;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated pattern {PatternId}", request.PatternId);

        return pattern.ToDto();
    }
}
