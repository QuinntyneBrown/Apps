using Incomes.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Incomes.Api.Features;

public record DeleteIncomeCommand(Guid IncomeId, Guid TenantId) : IRequest<bool>;

public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, bool>
{
    private readonly IIncomesDbContext _context;

    public DeleteIncomeCommandHandler(IIncomesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
    {
        var income = await _context.Incomes
            .FirstOrDefaultAsync(i => i.IncomeId == request.IncomeId && i.TenantId == request.TenantId, cancellationToken);

        if (income == null) return false;

        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
