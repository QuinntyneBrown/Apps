using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.VacationBudgets;

public record CreateVacationBudgetCommand : IRequest<VacationBudgetDto>
{
    public Guid TripId { get; init; }
    public string Category { get; init; } = string.Empty;
    public decimal AllocatedAmount { get; init; }
    public decimal? SpentAmount { get; init; }
}

public class CreateVacationBudgetCommandHandler : IRequestHandler<CreateVacationBudgetCommand, VacationBudgetDto>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<CreateVacationBudgetCommandHandler> _logger;

    public CreateVacationBudgetCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<CreateVacationBudgetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VacationBudgetDto> Handle(CreateVacationBudgetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating vacation budget for trip {TripId}, category: {Category}",
            request.TripId,
            request.Category);

        var budget = new VacationBudget
        {
            VacationBudgetId = Guid.NewGuid(),
            TripId = request.TripId,
            Category = request.Category,
            AllocatedAmount = request.AllocatedAmount,
            SpentAmount = request.SpentAmount,
            CreatedAt = DateTime.UtcNow,
        };

        _context.VacationBudgets.Add(budget);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created vacation budget {VacationBudgetId} for trip {TripId}",
            budget.VacationBudgetId,
            request.TripId);

        return budget.ToDto();
    }
}
