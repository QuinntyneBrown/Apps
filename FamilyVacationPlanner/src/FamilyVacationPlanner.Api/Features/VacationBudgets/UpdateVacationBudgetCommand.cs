using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.VacationBudgets;

public record UpdateVacationBudgetCommand : IRequest<VacationBudgetDto?>
{
    public Guid VacationBudgetId { get; init; }
    public string Category { get; init; } = string.Empty;
    public decimal AllocatedAmount { get; init; }
    public decimal? SpentAmount { get; init; }
}

public class UpdateVacationBudgetCommandHandler : IRequestHandler<UpdateVacationBudgetCommand, VacationBudgetDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<UpdateVacationBudgetCommandHandler> _logger;

    public UpdateVacationBudgetCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<UpdateVacationBudgetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VacationBudgetDto?> Handle(UpdateVacationBudgetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating vacation budget {VacationBudgetId}", request.VacationBudgetId);

        var budget = await _context.VacationBudgets
            .FirstOrDefaultAsync(vb => vb.VacationBudgetId == request.VacationBudgetId, cancellationToken);

        if (budget == null)
        {
            _logger.LogWarning("Vacation budget {VacationBudgetId} not found", request.VacationBudgetId);
            return null;
        }

        budget.Category = request.Category;
        budget.AllocatedAmount = request.AllocatedAmount;
        budget.SpentAmount = request.SpentAmount;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated vacation budget {VacationBudgetId}", request.VacationBudgetId);

        return budget.ToDto();
    }
}
