using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Expenses;

public record UpdateExpenseCommand : IRequest<ExpenseDto?>
{
    public Guid ExpenseId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime ExpenseDate { get; init; }
    public string? Category { get; init; }
    public string? Vendor { get; init; }
    public bool IsTaxDeductible { get; init; }
    public string? Notes { get; init; }
}

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, ExpenseDto?>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<UpdateExpenseCommandHandler> _logger;

    public UpdateExpenseCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<UpdateExpenseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ExpenseDto?> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating expense {ExpenseId}", request.ExpenseId);

        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.ExpenseId == request.ExpenseId, cancellationToken);

        if (expense == null)
        {
            _logger.LogWarning("Expense {ExpenseId} not found", request.ExpenseId);
            return null;
        }

        expense.Description = request.Description;
        expense.Amount = request.Amount;
        expense.ExpenseDate = request.ExpenseDate;
        expense.Category = request.Category;
        expense.Vendor = request.Vendor;
        expense.IsTaxDeductible = request.IsTaxDeductible;
        expense.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated expense {ExpenseId}", request.ExpenseId);

        return expense.ToDto();
    }
}
