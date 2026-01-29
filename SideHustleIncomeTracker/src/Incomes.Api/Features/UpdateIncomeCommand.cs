using Incomes.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Incomes.Api.Features;

public record UpdateIncomeCommand(Guid IncomeId, Guid TenantId, string? Description, decimal? Amount, DateTime? IncomeDate, string? Source) : IRequest<IncomeDto?>;

public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand, IncomeDto?>
{
    private readonly IIncomesDbContext _context;

    public UpdateIncomeCommandHandler(IIncomesDbContext context)
    {
        _context = context;
    }

    public async Task<IncomeDto?> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
    {
        var income = await _context.Incomes
            .FirstOrDefaultAsync(i => i.IncomeId == request.IncomeId && i.TenantId == request.TenantId, cancellationToken);

        if (income == null) return null;

        income.Update(request.Description, request.Amount, request.IncomeDate, request.Source);
        await _context.SaveChangesAsync(cancellationToken);

        return new IncomeDto(income.IncomeId, income.TenantId, income.UserId, income.BusinessId, income.Description, income.Amount, income.Source, income.IncomeDate, income.CreatedAt);
    }
}
