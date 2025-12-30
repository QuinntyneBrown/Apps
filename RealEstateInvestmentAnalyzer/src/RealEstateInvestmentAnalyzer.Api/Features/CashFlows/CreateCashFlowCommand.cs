using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.CashFlows;

public record CreateCashFlowCommand : IRequest<CashFlowDto>
{
    public Guid PropertyId { get; init; }
    public DateTime Date { get; init; }
    public decimal Income { get; init; }
    public decimal Expenses { get; init; }
    public string? Notes { get; init; }
}

public class CreateCashFlowCommandHandler : IRequestHandler<CreateCashFlowCommand, CashFlowDto>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<CreateCashFlowCommandHandler> _logger;

    public CreateCashFlowCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<CreateCashFlowCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CashFlowDto> Handle(CreateCashFlowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating cash flow for property {PropertyId}",
            request.PropertyId);

        var cashFlow = new CashFlow
        {
            CashFlowId = Guid.NewGuid(),
            PropertyId = request.PropertyId,
            Date = request.Date,
            Income = request.Income,
            Expenses = request.Expenses,
            Notes = request.Notes,
        };

        cashFlow.CalculateNetCashFlow();

        _context.CashFlows.Add(cashFlow);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created cash flow {CashFlowId}",
            cashFlow.CashFlowId);

        return cashFlow.ToDto();
    }
}
