using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.TaxEstimates;

public record CreateTaxEstimateCommand : IRequest<TaxEstimateDto>
{
    public Guid BusinessId { get; init; }
    public int TaxYear { get; init; }
    public int Quarter { get; init; }
    public decimal NetProfit { get; init; }
    public decimal SelfEmploymentTax { get; init; }
    public decimal IncomeTax { get; init; }
}

public class CreateTaxEstimateCommandHandler : IRequestHandler<CreateTaxEstimateCommand, TaxEstimateDto>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<CreateTaxEstimateCommandHandler> _logger;

    public CreateTaxEstimateCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<CreateTaxEstimateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaxEstimateDto> Handle(CreateTaxEstimateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating tax estimate for business {BusinessId}, year {TaxYear}, quarter {Quarter}",
            request.BusinessId,
            request.TaxYear,
            request.Quarter);

        var taxEstimate = new TaxEstimate
        {
            TaxEstimateId = Guid.NewGuid(),
            BusinessId = request.BusinessId,
            TaxYear = request.TaxYear,
            Quarter = request.Quarter,
            NetProfit = request.NetProfit,
            SelfEmploymentTax = request.SelfEmploymentTax,
            IncomeTax = request.IncomeTax,
            IsPaid = false,
        };

        taxEstimate.CalculateTotalTax();

        _context.TaxEstimates.Add(taxEstimate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created tax estimate {TaxEstimateId} for business {BusinessId}",
            taxEstimate.TaxEstimateId,
            request.BusinessId);

        return taxEstimate.ToDto();
    }
}
