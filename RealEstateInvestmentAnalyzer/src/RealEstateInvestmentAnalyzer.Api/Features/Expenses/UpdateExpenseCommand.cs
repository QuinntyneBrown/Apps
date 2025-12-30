using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Expenses;

public record UpdateExpenseCommand : IRequest<ExpenseDto?>
{
    public Guid ExpenseId { get; init; }
    public Guid PropertyId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public string Category { get; init; } = string.Empty;
    public bool IsRecurring { get; init; }
    public string? Notes { get; init; }
}

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, ExpenseDto?>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<UpdateExpenseCommandHandler> _logger;

    public UpdateExpenseCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
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

        expense.PropertyId = request.PropertyId;
        expense.Description = request.Description;
        expense.Amount = request.Amount;
        expense.Date = request.Date;
        expense.Category = request.Category;
        expense.IsRecurring = request.IsRecurring;
        expense.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated expense {ExpenseId}", request.ExpenseId);

        return expense.ToDto();
    }
}
