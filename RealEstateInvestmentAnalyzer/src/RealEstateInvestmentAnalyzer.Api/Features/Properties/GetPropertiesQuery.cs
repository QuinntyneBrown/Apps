using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Properties;

public record GetPropertiesQuery : IRequest<IEnumerable<PropertyDto>>
{
    public PropertyType? PropertyType { get; init; }
}

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<PropertyDto>>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<GetPropertiesQueryHandler> _logger;

    public GetPropertiesQueryHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<GetPropertiesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting properties");

        var query = _context.Properties.AsNoTracking();

        if (request.PropertyType.HasValue)
        {
            query = query.Where(p => p.PropertyType == request.PropertyType.Value);
        }

        var properties = await query
            .OrderBy(p => p.Address)
            .ToListAsync(cancellationToken);

        return properties.Select(p => p.ToDto());
    }
}
