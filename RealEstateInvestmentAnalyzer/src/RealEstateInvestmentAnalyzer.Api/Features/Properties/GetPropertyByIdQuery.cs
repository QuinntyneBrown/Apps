using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Properties;

public record GetPropertyByIdQuery : IRequest<PropertyDto?>
{
    public Guid PropertyId { get; init; }
}

public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto?>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<GetPropertyByIdQueryHandler> _logger;

    public GetPropertyByIdQueryHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<GetPropertyByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PropertyDto?> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting property {PropertyId}", request.PropertyId);

        var property = await _context.Properties
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PropertyId == request.PropertyId, cancellationToken);

        return property?.ToDto();
    }
}
