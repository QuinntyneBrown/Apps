using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Properties;

public record DeletePropertyCommand : IRequest<bool>
{
    public Guid PropertyId { get; init; }
}

public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, bool>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<DeletePropertyCommandHandler> _logger;

    public DeletePropertyCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<DeletePropertyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting property {PropertyId}", request.PropertyId);

        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.PropertyId == request.PropertyId, cancellationToken);

        if (property == null)
        {
            _logger.LogWarning("Property {PropertyId} not found", request.PropertyId);
            return false;
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted property {PropertyId}", request.PropertyId);

        return true;
    }
}
