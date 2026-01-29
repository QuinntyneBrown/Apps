using TaxYears.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TaxYears.Api.Features;

public record DeleteTaxYearCommand(Guid TaxYearId) : IRequest<bool>;

public class DeleteTaxYearCommandHandler : IRequestHandler<DeleteTaxYearCommand, bool>
{
    private readonly ITaxYearsDbContext _context;
    private readonly ILogger<DeleteTaxYearCommandHandler> _logger;

    public DeleteTaxYearCommandHandler(ITaxYearsDbContext context, ILogger<DeleteTaxYearCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTaxYearCommand request, CancellationToken cancellationToken)
    {
        var taxYear = await _context.TaxYears
            .FirstOrDefaultAsync(t => t.TaxYearId == request.TaxYearId, cancellationToken);

        if (taxYear == null) return false;

        _context.TaxYears.Remove(taxYear);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("TaxYear deleted: {TaxYearId}", request.TaxYearId);

        return true;
    }
}
