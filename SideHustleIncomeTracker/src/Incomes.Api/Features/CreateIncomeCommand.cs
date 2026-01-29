using Incomes.Core;
using Incomes.Core.Models;
using MediatR;

namespace Incomes.Api.Features;

public record CreateIncomeCommand(Guid TenantId, Guid UserId, Guid BusinessId, string Description, decimal Amount, DateTime IncomeDate, string? Source) : IRequest<IncomeDto>;

public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, IncomeDto>
{
    private readonly IIncomesDbContext _context;

    public CreateIncomeCommandHandler(IIncomesDbContext context)
    {
        _context = context;
    }

    public async Task<IncomeDto> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
    {
        var income = new Income(request.TenantId, request.UserId, request.BusinessId, request.Description, request.Amount, request.IncomeDate, request.Source);

        _context.Incomes.Add(income);
        await _context.SaveChangesAsync(cancellationToken);

        return new IncomeDto(income.IncomeId, income.TenantId, income.UserId, income.BusinessId, income.Description, income.Amount, income.Source, income.IncomeDate, income.CreatedAt);
    }
}
