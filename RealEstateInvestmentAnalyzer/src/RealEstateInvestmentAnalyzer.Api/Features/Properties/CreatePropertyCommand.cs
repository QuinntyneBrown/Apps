using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Properties;

public record CreatePropertyCommand : IRequest<PropertyDto>
{
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

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, PropertyDto>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<CreatePropertyCommandHandler> _logger;

    public CreatePropertyCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<CreatePropertyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PropertyDto> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating property at {Address}",
            request.Address);

        var property = new Property
        {
            PropertyId = Guid.NewGuid(),
            Address = request.Address,
            PropertyType = request.PropertyType,
            PurchasePrice = request.PurchasePrice,
            PurchaseDate = request.PurchaseDate,
            CurrentValue = request.CurrentValue,
            SquareFeet = request.SquareFeet,
            Bedrooms = request.Bedrooms,
            Bathrooms = request.Bathrooms,
            Notes = request.Notes,
        };

        _context.Properties.Add(property);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created property {PropertyId}",
            property.PropertyId);

        return property.ToDto();
    }
}
