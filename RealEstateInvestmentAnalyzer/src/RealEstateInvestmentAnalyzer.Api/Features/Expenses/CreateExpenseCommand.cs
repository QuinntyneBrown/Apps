using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Expenses;

public record CreateExpenseCommand : IRequest<ExpenseDto>
{
    public Guid PropertyId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public string Category { get; init; } = string.Empty;
    public bool IsRecurring { get; init; }
    public string? Notes { get; init; }
}

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ExpenseDto>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<CreateExpenseCommandHandler> _logger;

    public CreateExpenseCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<CreateExpenseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ExpenseDto> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating expense for property {PropertyId}, description: {Description}",
            request.PropertyId,
            request.Description);

        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            PropertyId = request.PropertyId,
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            Category = request.Category,
            IsRecurring = request.IsRecurring,
            Notes = request.Notes,
        };

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created expense {ExpenseId}",
            expense.ExpenseId);

        return expense.ToDto();
    }
}
