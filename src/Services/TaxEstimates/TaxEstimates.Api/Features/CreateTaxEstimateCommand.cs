using TaxEstimates.Core;
using TaxEstimates.Core.Models;
using MediatR;

namespace TaxEstimates.Api.Features;

public record CreateTaxEstimateCommand(Guid TenantId, Guid UserId, int Year, int Quarter, decimal TotalIncome, decimal TotalExpenses, decimal TaxRate, Guid? BusinessId) : IRequest<TaxEstimateDto>;

public class CreateTaxEstimateCommandHandler : IRequestHandler<CreateTaxEstimateCommand, TaxEstimateDto>
{
    private readonly ITaxEstimatesDbContext _context;

    public CreateTaxEstimateCommandHandler(ITaxEstimatesDbContext context)
    {
        _context = context;
    }

    public async Task<TaxEstimateDto> Handle(CreateTaxEstimateCommand request, CancellationToken cancellationToken)
    {
        var estimate = new TaxEstimate(request.TenantId, request.UserId, request.Year, request.Quarter, request.TotalIncome, request.TotalExpenses, request.TaxRate, request.BusinessId);

        _context.TaxEstimates.Add(estimate);
        await _context.SaveChangesAsync(cancellationToken);

        return new TaxEstimateDto(estimate.TaxEstimateId, estimate.TenantId, estimate.UserId, estimate.BusinessId, estimate.Year, estimate.Quarter, estimate.TotalIncome, estimate.TotalExpenses, estimate.NetIncome, estimate.EstimatedTax, estimate.TaxRate, estimate.CalculatedAt);
    }
}
