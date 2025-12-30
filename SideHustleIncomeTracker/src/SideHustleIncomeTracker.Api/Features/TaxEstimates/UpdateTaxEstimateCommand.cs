using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.TaxEstimates;

public record UpdateTaxEstimateCommand : IRequest<TaxEstimateDto?>
{
    public Guid TaxEstimateId { get; init; }
    public int TaxYear { get; init; }
    public int Quarter { get; init; }
    public decimal NetProfit { get; init; }
    public decimal SelfEmploymentTax { get; init; }
    public decimal IncomeTax { get; init; }
    public bool IsPaid { get; init; }
    public DateTime? PaymentDate { get; init; }
}

public class UpdateTaxEstimateCommandHandler : IRequestHandler<UpdateTaxEstimateCommand, TaxEstimateDto?>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<UpdateTaxEstimateCommandHandler> _logger;

    public UpdateTaxEstimateCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<UpdateTaxEstimateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaxEstimateDto?> Handle(UpdateTaxEstimateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating tax estimate {TaxEstimateId}", request.TaxEstimateId);

        var taxEstimate = await _context.TaxEstimates
            .FirstOrDefaultAsync(t => t.TaxEstimateId == request.TaxEstimateId, cancellationToken);

        if (taxEstimate == null)
        {
            _logger.LogWarning("Tax estimate {TaxEstimateId} not found", request.TaxEstimateId);
            return null;
        }

        taxEstimate.TaxYear = request.TaxYear;
        taxEstimate.Quarter = request.Quarter;
        taxEstimate.NetProfit = request.NetProfit;
        taxEstimate.SelfEmploymentTax = request.SelfEmploymentTax;
        taxEstimate.IncomeTax = request.IncomeTax;
        taxEstimate.IsPaid = request.IsPaid;
        taxEstimate.PaymentDate = request.PaymentDate;

        taxEstimate.CalculateTotalTax();

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated tax estimate {TaxEstimateId}", request.TaxEstimateId);

        return taxEstimate.ToDto();
    }
}
