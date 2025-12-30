// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.RefinanceScenario;

/// <summary>
/// Command to create a new refinance scenario.
/// </summary>
public record CreateRefinanceScenarioCommand : IRequest<RefinanceScenarioDto>
{
    public Guid MortgageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal NewInterestRate { get; set; }
    public int NewLoanTermYears { get; set; }
    public decimal RefinancingCosts { get; set; }
    public decimal NewMonthlyPayment { get; set; }
    public decimal MonthlySavings { get; set; }
    public int BreakEvenMonths { get; set; }
    public decimal TotalSavings { get; set; }
}

/// <summary>
/// Handler for CreateRefinanceScenarioCommand.
/// </summary>
public class CreateRefinanceScenarioCommandHandler : IRequestHandler<CreateRefinanceScenarioCommand, RefinanceScenarioDto>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public CreateRefinanceScenarioCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<RefinanceScenarioDto> Handle(CreateRefinanceScenarioCommand request, CancellationToken cancellationToken)
    {
        var scenario = new Core.RefinanceScenario
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = request.MortgageId,
            Name = request.Name,
            NewInterestRate = request.NewInterestRate,
            NewLoanTermYears = request.NewLoanTermYears,
            RefinancingCosts = request.RefinancingCosts,
            NewMonthlyPayment = request.NewMonthlyPayment,
            MonthlySavings = request.MonthlySavings,
            BreakEvenMonths = request.BreakEvenMonths,
            TotalSavings = request.TotalSavings,
            CreatedAt = DateTime.UtcNow
        };

        _context.RefinanceScenarios.Add(scenario);
        await _context.SaveChangesAsync(cancellationToken);

        return scenario.ToDto();
    }
}
