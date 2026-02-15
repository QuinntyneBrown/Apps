using Incomes.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Incomes.Api.Features;

public record GetIncomesQuery(Guid TenantId, Guid BusinessId) : IRequest<IEnumerable<IncomeDto>>;

public class GetIncomesQueryHandler : IRequestHandler<GetIncomesQuery, IEnumerable<IncomeDto>>
{
    private readonly IIncomesDbContext _context;

    public GetIncomesQueryHandler(IIncomesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IncomeDto>> Handle(GetIncomesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Incomes
            .Where(i => i.TenantId == request.TenantId && i.BusinessId == request.BusinessId)
            .Select(i => new IncomeDto(i.IncomeId, i.TenantId, i.UserId, i.BusinessId, i.Description, i.Amount, i.Source, i.IncomeDate, i.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
