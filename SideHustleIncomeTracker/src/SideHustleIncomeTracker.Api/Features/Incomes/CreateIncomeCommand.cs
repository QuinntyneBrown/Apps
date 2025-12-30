using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Incomes;

public record CreateIncomeCommand : IRequest<IncomeDto>
{
    public Guid BusinessId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime IncomeDate { get; init; }
    public string? Client { get; init; }
    public string? InvoiceNumber { get; init; }
    public bool IsPaid { get; init; }
    public string? Notes { get; init; }
}

public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, IncomeDto>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<CreateIncomeCommandHandler> _logger;

    public CreateIncomeCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<CreateIncomeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IncomeDto> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating income for business {BusinessId}, amount: {Amount}",
            request.BusinessId,
            request.Amount);

        var income = new Income
        {
            IncomeId = Guid.NewGuid(),
            BusinessId = request.BusinessId,
            Description = request.Description,
            Amount = request.Amount,
            IncomeDate = request.IncomeDate,
            Client = request.Client,
            InvoiceNumber = request.InvoiceNumber,
            IsPaid = request.IsPaid,
            Notes = request.Notes,
        };

        _context.Incomes.Add(income);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created income {IncomeId} for business {BusinessId}",
            income.IncomeId,
            request.BusinessId);

        return income.ToDto();
    }
}
