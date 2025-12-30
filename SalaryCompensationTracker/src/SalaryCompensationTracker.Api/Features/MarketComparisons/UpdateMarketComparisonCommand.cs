using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.MarketComparisons;

public record UpdateMarketComparisonCommand : IRequest<MarketComparisonDto?>
{
    public Guid MarketComparisonId { get; init; }
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

public class UpdateMarketComparisonCommandHandler : IRequestHandler<UpdateMarketComparisonCommand, MarketComparisonDto?>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<UpdateMarketComparisonCommandHandler> _logger;

    public UpdateMarketComparisonCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<UpdateMarketComparisonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MarketComparisonDto?> Handle(UpdateMarketComparisonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating market comparison {MarketComparisonId}", request.MarketComparisonId);

        var marketComparison = await _context.MarketComparisons
            .FirstOrDefaultAsync(m => m.MarketComparisonId == request.MarketComparisonId, cancellationToken);

        if (marketComparison == null)
        {
            _logger.LogWarning("Market comparison {MarketComparisonId} not found", request.MarketComparisonId);
            return null;
        }

        marketComparison.JobTitle = request.JobTitle;
        marketComparison.Location = request.Location;
        marketComparison.ExperienceLevel = request.ExperienceLevel;
        marketComparison.MinSalary = request.MinSalary;
        marketComparison.MaxSalary = request.MaxSalary;
        marketComparison.MedianSalary = request.MedianSalary;
        marketComparison.DataSource = request.DataSource;
        marketComparison.ComparisonDate = request.ComparisonDate;
        marketComparison.Notes = request.Notes;
        marketComparison.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated market comparison {MarketComparisonId}", request.MarketComparisonId);

        return marketComparison.ToDto();
    }
}
