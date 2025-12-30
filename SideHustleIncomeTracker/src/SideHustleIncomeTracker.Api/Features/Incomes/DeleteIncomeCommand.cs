using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Incomes;

public record DeleteIncomeCommand : IRequest<bool>
{
    public Guid IncomeId { get; init; }
}

public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, bool>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<DeleteIncomeCommandHandler> _logger;

    public DeleteIncomeCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<DeleteIncomeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting income {IncomeId}", request.IncomeId);

        var income = await _context.Incomes
            .FirstOrDefaultAsync(i => i.IncomeId == request.IncomeId, cancellationToken);

        if (income == null)
        {
            _logger.LogWarning("Income {IncomeId} not found", request.IncomeId);
            return false;
        }

        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted income {IncomeId}", request.IncomeId);

        return true;
    }
}
