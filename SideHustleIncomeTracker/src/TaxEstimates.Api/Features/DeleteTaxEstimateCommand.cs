using TaxEstimates.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TaxEstimates.Api.Features;

public record DeleteTaxEstimateCommand(Guid TaxEstimateId, Guid TenantId) : IRequest<bool>;

public class DeleteTaxEstimateCommandHandler : IRequestHandler<DeleteTaxEstimateCommand, bool>
{
    private readonly ITaxEstimatesDbContext _context;

    public DeleteTaxEstimateCommandHandler(ITaxEstimatesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTaxEstimateCommand request, CancellationToken cancellationToken)
    {
        var estimate = await _context.TaxEstimates
            .FirstOrDefaultAsync(t => t.TaxEstimateId == request.TaxEstimateId && t.TenantId == request.TenantId, cancellationToken);

        if (estimate == null) return false;

        _context.TaxEstimates.Remove(estimate);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
