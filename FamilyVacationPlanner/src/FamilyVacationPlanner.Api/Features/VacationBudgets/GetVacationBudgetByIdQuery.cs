using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.VacationBudgets;

public record GetVacationBudgetByIdQuery : IRequest<VacationBudgetDto?>
{
    public Guid VacationBudgetId { get; init; }
}

public class GetVacationBudgetByIdQueryHandler : IRequestHandler<GetVacationBudgetByIdQuery, VacationBudgetDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetVacationBudgetByIdQueryHandler> _logger;

    public GetVacationBudgetByIdQueryHandler(
        IFamilyVacationPlannerContext context,
        ILogger<GetVacationBudgetByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VacationBudgetDto?> Handle(GetVacationBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vacation budget {VacationBudgetId}", request.VacationBudgetId);

        var budget = await _context.VacationBudgets
            .AsNoTracking()
            .FirstOrDefaultAsync(vb => vb.VacationBudgetId == request.VacationBudgetId, cancellationToken);

        if (budget == null)
        {
            _logger.LogWarning("Vacation budget {VacationBudgetId} not found", request.VacationBudgetId);
            return null;
        }

        return budget.ToDto();
    }
}
