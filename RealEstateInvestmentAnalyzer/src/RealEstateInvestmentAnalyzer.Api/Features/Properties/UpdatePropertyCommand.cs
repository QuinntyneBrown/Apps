using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Properties;

public record UpdatePropertyCommand : IRequest<PropertyDto?>
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
}

public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, PropertyDto?>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<UpdatePropertyCommandHandler> _logger;

    public UpdatePropertyCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<UpdatePropertyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PropertyDto?> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating property {PropertyId}", request.PropertyId);

        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.PropertyId == request.PropertyId, cancellationToken);

        if (property == null)
        {
            _logger.LogWarning("Property {PropertyId} not found", request.PropertyId);
            return null;
        }

        property.Address = request.Address;
        property.PropertyType = request.PropertyType;
        property.PurchasePrice = request.PurchasePrice;
        property.PurchaseDate = request.PurchaseDate;
        property.CurrentValue = request.CurrentValue;
        property.SquareFeet = request.SquareFeet;
        property.Bedrooms = request.Bedrooms;
        property.Bathrooms = request.Bathrooms;
        property.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated property {PropertyId}", request.PropertyId);

        return property.ToDto();
    }
}
