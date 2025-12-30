using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Expenses;

public record CreateExpenseCommand : IRequest<ExpenseDto>
{
    public Guid BusinessId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime ExpenseDate { get; init; }
    public string? Category { get; init; }
    public string? Vendor { get; init; }
    public bool IsTaxDeductible { get; init; } = true;
    public string? Notes { get; init; }
}

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ExpenseDto>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<CreateExpenseCommandHandler> _logger;

    public CreateExpenseCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<CreateExpenseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ExpenseDto> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating expense for business {BusinessId}, amount: {Amount}",
            request.BusinessId,
            request.Amount);

        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            BusinessId = request.BusinessId,
            Description = request.Description,
            Amount = request.Amount,
            ExpenseDate = request.ExpenseDate,
            Category = request.Category,
            Vendor = request.Vendor,
            IsTaxDeductible = request.IsTaxDeductible,
            Notes = request.Notes,
        };

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created expense {ExpenseId} for business {BusinessId}",
            expense.ExpenseId,
            request.BusinessId);

        return expense.ToDto();
    }
}
