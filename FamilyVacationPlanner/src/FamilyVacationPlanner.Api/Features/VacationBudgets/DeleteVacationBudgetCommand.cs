using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.VacationBudgets;

public record DeleteVacationBudgetCommand : IRequest<bool>
{
    public Guid VacationBudgetId { get; init; }
}

public class DeleteVacationBudgetCommandHandler : IRequestHandler<DeleteVacationBudgetCommand, bool>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<DeleteVacationBudgetCommandHandler> _logger;

    public DeleteVacationBudgetCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<DeleteVacationBudgetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteVacationBudgetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting vacation budget {VacationBudgetId}", request.VacationBudgetId);

        var budget = await _context.VacationBudgets
            .FirstOrDefaultAsync(vb => vb.VacationBudgetId == request.VacationBudgetId, cancellationToken);

        if (budget == null)
        {
            _logger.LogWarning("Vacation budget {VacationBudgetId} not found", request.VacationBudgetId);
            return false;
        }

        _context.VacationBudgets.Remove(budget);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted vacation budget {VacationBudgetId}", request.VacationBudgetId);

        return true;
    }
}
