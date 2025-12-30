using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Compensations;

public record DeleteCompensationCommand : IRequest<bool>
{
    public Guid CompensationId { get; init; }
}

public class DeleteCompensationCommandHandler : IRequestHandler<DeleteCompensationCommand, bool>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<DeleteCompensationCommandHandler> _logger;

    public DeleteCompensationCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<DeleteCompensationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCompensationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting compensation {CompensationId}", request.CompensationId);

        var compensation = await _context.Compensations
            .FirstOrDefaultAsync(c => c.CompensationId == request.CompensationId, cancellationToken);

        if (compensation == null)
        {
            _logger.LogWarning("Compensation {CompensationId} not found", request.CompensationId);
            return false;
        }

        _context.Compensations.Remove(compensation);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted compensation {CompensationId}", request.CompensationId);

        return true;
    }
}
