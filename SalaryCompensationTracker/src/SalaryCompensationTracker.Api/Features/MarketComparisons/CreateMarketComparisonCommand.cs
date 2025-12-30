using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.MarketComparisons;

public record CreateMarketComparisonCommand : IRequest<MarketComparisonDto>
{
    public Guid UserId { get; init; }
    public string JobTitle { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string? ExperienceLevel { get; init; }
    public decimal? MinSalary { get; init; }
    public decimal? MaxSalary { get; init; }
    public decimal? MedianSalary { get; init; }
    public string? DataSource { get; init; }
    public DateTime ComparisonDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateMarketComparisonCommandHandler : IRequestHandler<CreateMarketComparisonCommand, MarketComparisonDto>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<CreateMarketComparisonCommandHandler> _logger;

    public CreateMarketComparisonCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<CreateMarketComparisonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MarketComparisonDto> Handle(CreateMarketComparisonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating market comparison for user {UserId}, job title: {JobTitle}",
            request.UserId,
            request.JobTitle);

        var marketComparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            UserId = request.UserId,
            JobTitle = request.JobTitle,
            Location = request.Location,
            ExperienceLevel = request.ExperienceLevel,
            MinSalary = request.MinSalary,
            MaxSalary = request.MaxSalary,
            MedianSalary = request.MedianSalary,
            DataSource = request.DataSource,
            ComparisonDate = request.ComparisonDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.MarketComparisons.Add(marketComparison);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created market comparison {MarketComparisonId} for user {UserId}",
            marketComparison.MarketComparisonId,
            request.UserId);

        return marketComparison.ToDto();
    }
}
