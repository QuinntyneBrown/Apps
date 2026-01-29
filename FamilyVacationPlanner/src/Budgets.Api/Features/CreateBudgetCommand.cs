using Budgets.Core;
using Budgets.Core.Models;
using MediatR;

namespace Budgets.Api.Features;

public record CreateBudgetCommand : IRequest<BudgetDto>
{
    public Guid TenantId { get; init; }
    public Guid TripId { get; init; }
    public string Category { get; init; } = string.Empty;
    public decimal AllocatedAmount { get; init; }
}

public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
{
    private readonly IBudgetsDbContext _context;

    public CreateBudgetCommandHandler(IBudgetsDbContext context)
    {
        _context = context;
    }

    public async Task<BudgetDto> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = new VacationBudget
        {
            VacationBudgetId = Guid.NewGuid(),
            TenantId = request.TenantId,
            TripId = request.TripId,
            Category = request.Category,
            AllocatedAmount = request.AllocatedAmount,
            SpentAmount = 0,
            CreatedAt = DateTime.UtcNow
        };

        _context.VacationBudgets.Add(budget);
        await _context.SaveChangesAsync(cancellationToken);

        return new BudgetDto
        {
            VacationBudgetId = budget.VacationBudgetId,
            TripId = budget.TripId,
            Category = budget.Category,
            AllocatedAmount = budget.AllocatedAmount,
            SpentAmount = budget.SpentAmount,
            CreatedAt = budget.CreatedAt
        };
    }
}
