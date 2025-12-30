using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Incomes;

public record UpdateIncomeCommand : IRequest<IncomeDto?>
{
    public Guid IncomeId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime IncomeDate { get; init; }
    public string? Client { get; init; }
    public string? InvoiceNumber { get; init; }
    public bool IsPaid { get; init; }
    public string? Notes { get; init; }
}

public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand, IncomeDto?>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<UpdateIncomeCommandHandler> _logger;

    public UpdateIncomeCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<UpdateIncomeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IncomeDto?> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating income {IncomeId}", request.IncomeId);

        var income = await _context.Incomes
            .FirstOrDefaultAsync(i => i.IncomeId == request.IncomeId, cancellationToken);

        if (income == null)
        {
            _logger.LogWarning("Income {IncomeId} not found", request.IncomeId);
            return null;
        }

        income.Description = request.Description;
        income.Amount = request.Amount;
        income.IncomeDate = request.IncomeDate;
        income.Client = request.Client;
        income.InvoiceNumber = request.InvoiceNumber;
        income.IsPaid = request.IsPaid;
        income.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated income {IncomeId}", request.IncomeId);

        return income.ToDto();
    }
}
