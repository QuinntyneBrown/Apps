using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Benefits;

public record DeleteBenefitCommand : IRequest<bool>
{
    public Guid BenefitId { get; init; }
}

public class DeleteBenefitCommandHandler : IRequestHandler<DeleteBenefitCommand, bool>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<DeleteBenefitCommandHandler> _logger;

    public DeleteBenefitCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<DeleteBenefitCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteBenefitCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting benefit {BenefitId}", request.BenefitId);

        var benefit = await _context.Benefits
            .FirstOrDefaultAsync(b => b.BenefitId == request.BenefitId, cancellationToken);

        if (benefit == null)
        {
            _logger.LogWarning("Benefit {BenefitId} not found", request.BenefitId);
            return false;
        }

        _context.Benefits.Remove(benefit);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted benefit {BenefitId}", request.BenefitId);

        return true;
    }
}
