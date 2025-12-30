using SleepQualityTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Patterns;

public record CreatePatternCommand : IRequest<PatternDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string PatternType { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int ConfidenceLevel { get; init; }
    public string? Insights { get; init; }
}

public class CreatePatternCommandHandler : IRequestHandler<CreatePatternCommand, PatternDto>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<CreatePatternCommandHandler> _logger;

    public CreatePatternCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<CreatePatternCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PatternDto> Handle(CreatePatternCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating pattern for user {UserId}, type: {PatternType}",
            request.UserId,
            request.PatternType);

        var pattern = new Pattern
        {
            PatternId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            PatternType = request.PatternType,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ConfidenceLevel = request.ConfidenceLevel,
            Insights = request.Insights,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Patterns.Add(pattern);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created pattern {PatternId} for user {UserId}",
            pattern.PatternId,
            request.UserId);

        return pattern.ToDto();
    }
}
