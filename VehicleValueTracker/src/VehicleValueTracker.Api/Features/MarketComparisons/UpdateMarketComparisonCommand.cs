using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.MarketComparisons;

public record UpdateMarketComparisonCommand : IRequest<MarketComparisonDto?>
{
    public Guid MarketComparisonId { get; init; }
    public Guid VehicleId { get; init; }
    public DateTime ComparisonDate { get; init; }
    public string ListingSource { get; init; } = string.Empty;
    public int ComparableYear { get; init; }
    public string ComparableMake { get; init; } = string.Empty;
    public string ComparableModel { get; init; } = string.Empty;
    public string? ComparableTrim { get; init; }
    public decimal ComparableMileage { get; init; }
    public decimal AskingPrice { get; init; }
    public string? Location { get; init; }
    public string? Condition { get; init; }
    public string? ListingUrl { get; init; }
    public int? DaysOnMarket { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
}

public class UpdateMarketComparisonCommandHandler : IRequestHandler<UpdateMarketComparisonCommand, MarketComparisonDto?>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<UpdateMarketComparisonCommandHandler> _logger;

    public UpdateMarketComparisonCommandHandler(
        IVehicleValueTrackerContext context,
        ILogger<UpdateMarketComparisonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MarketComparisonDto?> Handle(UpdateMarketComparisonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating market comparison {MarketComparisonId}", request.MarketComparisonId);

        var comparison = await _context.MarketComparisons
            .FirstOrDefaultAsync(m => m.MarketComparisonId == request.MarketComparisonId, cancellationToken);

        if (comparison == null)
        {
            _logger.LogWarning("Market comparison {MarketComparisonId} not found", request.MarketComparisonId);
            return null;
        }

        comparison.VehicleId = request.VehicleId;
        comparison.ComparisonDate = request.ComparisonDate;
        comparison.ListingSource = request.ListingSource;
        comparison.ComparableYear = request.ComparableYear;
        comparison.ComparableMake = request.ComparableMake;
        comparison.ComparableModel = request.ComparableModel;
        comparison.ComparableTrim = request.ComparableTrim;
        comparison.ComparableMileage = request.ComparableMileage;
        comparison.AskingPrice = request.AskingPrice;
        comparison.Location = request.Location;
        comparison.Condition = request.Condition;
        comparison.ListingUrl = request.ListingUrl;
        comparison.DaysOnMarket = request.DaysOnMarket;
        comparison.Notes = request.Notes;
        comparison.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated market comparison {MarketComparisonId}", request.MarketComparisonId);

        return comparison.ToDto();
    }
}
