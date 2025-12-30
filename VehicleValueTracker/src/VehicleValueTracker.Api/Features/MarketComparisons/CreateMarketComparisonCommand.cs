using VehicleValueTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.MarketComparisons;

public record CreateMarketComparisonCommand : IRequest<MarketComparisonDto>
{
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
    public bool IsActive { get; init; } = true;
}

public class CreateMarketComparisonCommandHandler : IRequestHandler<CreateMarketComparisonCommand, MarketComparisonDto>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<CreateMarketComparisonCommandHandler> _logger;

    public CreateMarketComparisonCommandHandler(
        IVehicleValueTrackerContext context,
        ILogger<CreateMarketComparisonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MarketComparisonDto> Handle(CreateMarketComparisonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating market comparison for vehicle {VehicleId}",
            request.VehicleId);

        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            ComparisonDate = request.ComparisonDate,
            ListingSource = request.ListingSource,
            ComparableYear = request.ComparableYear,
            ComparableMake = request.ComparableMake,
            ComparableModel = request.ComparableModel,
            ComparableTrim = request.ComparableTrim,
            ComparableMileage = request.ComparableMileage,
            AskingPrice = request.AskingPrice,
            Location = request.Location,
            Condition = request.Condition,
            ListingUrl = request.ListingUrl,
            DaysOnMarket = request.DaysOnMarket,
            Notes = request.Notes,
            IsActive = request.IsActive,
        };

        _context.MarketComparisons.Add(comparison);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created market comparison {MarketComparisonId} for vehicle {VehicleId}",
            comparison.MarketComparisonId,
            request.VehicleId);

        return comparison.ToDto();
    }
}
