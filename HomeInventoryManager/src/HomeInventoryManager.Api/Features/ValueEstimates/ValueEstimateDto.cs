using HomeInventoryManager.Core;

namespace HomeInventoryManager.Api.Features.ValueEstimates;

public record ValueEstimateDto
{
    public Guid ValueEstimateId { get; init; }
    public Guid ItemId { get; init; }
    public decimal EstimatedValue { get; init; }
    public DateTime EstimationDate { get; init; }
    public string? Source { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ValueEstimateExtensions
{
    public static ValueEstimateDto ToDto(this ValueEstimate valueEstimate)
    {
        return new ValueEstimateDto
        {
            ValueEstimateId = valueEstimate.ValueEstimateId,
            ItemId = valueEstimate.ItemId,
            EstimatedValue = valueEstimate.EstimatedValue,
            EstimationDate = valueEstimate.EstimationDate,
            Source = valueEstimate.Source,
            Notes = valueEstimate.Notes,
            CreatedAt = valueEstimate.CreatedAt,
        };
    }
}
