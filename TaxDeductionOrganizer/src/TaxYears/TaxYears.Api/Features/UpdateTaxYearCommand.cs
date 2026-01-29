using TaxYears.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TaxYears.Api.Features;

public record UpdateTaxYearCommand(Guid TaxYearId, bool? IsClosed, string? Notes) : IRequest<TaxYearDto?>;

public class UpdateTaxYearCommandHandler : IRequestHandler<UpdateTaxYearCommand, TaxYearDto?>
{
    private readonly ITaxYearsDbContext _context;
    private readonly ILogger<UpdateTaxYearCommandHandler> _logger;

    public UpdateTaxYearCommandHandler(ITaxYearsDbContext context, ILogger<UpdateTaxYearCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaxYearDto?> Handle(UpdateTaxYearCommand request, CancellationToken cancellationToken)
    {
        var taxYear = await _context.TaxYears
            .FirstOrDefaultAsync(t => t.TaxYearId == request.TaxYearId, cancellationToken);

        if (taxYear == null) return null;

        if (request.IsClosed.HasValue)
        {
            if (request.IsClosed.Value) taxYear.Close();
            else taxYear.Reopen();
        }
        if (request.Notes != null) taxYear.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("TaxYear updated: {TaxYearId}", taxYear.TaxYearId);

        return taxYear.ToDto();
    }
}
