using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Api.Features.Properties;

public record PropertyDto
{
    public Guid PropertyId { get; init; }
    public string Address { get; init; } = string.Empty;
    public PropertyType PropertyType { get; init; }
    public decimal PurchasePrice { get; init; }
    public DateTime PurchaseDate { get; init; }
    public decimal CurrentValue { get; init; }
    public int SquareFeet { get; init; }
    public int Bedrooms { get; init; }
    public int Bathrooms { get; init; }
    public string? Notes { get; init; }
    public decimal Equity { get; init; }
    public decimal ROI { get; init; }
}

public static class PropertyExtensions
{
    public static PropertyDto ToDto(this Property property)
    {
        return new PropertyDto
        {
            PropertyId = property.PropertyId,
            Address = property.Address,
            PropertyType = property.PropertyType,
            PurchasePrice = property.PurchasePrice,
            PurchaseDate = property.PurchaseDate,
            CurrentValue = property.CurrentValue,
            SquareFeet = property.SquareFeet,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Notes = property.Notes,
            Equity = property.CalculateEquity(),
            ROI = property.CalculateROI(),
        };
    }
}
