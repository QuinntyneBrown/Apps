using Incomes.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Incomes.Api.Features;

public record GetIncomeByIdQuery(Guid IncomeId, Guid TenantId) : IRequest<IncomeDto?>;

public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQuery, IncomeDto?>
{
    private readonly IIncomesDbContext _context;

    public GetIncomeByIdQueryHandler(IIncomesDbContext context)
    {
        _context = context;
    }

    public async Task<IncomeDto?> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
    {
        var income = await _context.Incomes
            .FirstOrDefaultAsync(i => i.IncomeId == request.IncomeId && i.TenantId == request.TenantId, cancellationToken);

        return income == null ? null : new IncomeDto(income.IncomeId, income.TenantId, income.UserId, income.BusinessId, income.Description, income.Amount, income.Source, income.IncomeDate, income.CreatedAt);
    }
}
