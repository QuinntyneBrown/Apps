// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.RefinanceScenario;

/// <summary>
/// Command to update an existing refinance scenario.
/// </summary>
public record UpdateRefinanceScenarioCommand : IRequest<RefinanceScenarioDto>
{
    public Guid RefinanceScenarioId { get; set; }
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
/// Handler for UpdateRefinanceScenarioCommand.
/// </summary>
public class UpdateRefinanceScenarioCommandHandler : IRequestHandler<UpdateRefinanceScenarioCommand, RefinanceScenarioDto>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public UpdateRefinanceScenarioCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<RefinanceScenarioDto> Handle(UpdateRefinanceScenarioCommand request, CancellationToken cancellationToken)
    {
        var scenario = await _context.RefinanceScenarios
            .FirstOrDefaultAsync(s => s.RefinanceScenarioId == request.RefinanceScenarioId, cancellationToken);

        if (scenario == null)
        {
            throw new Exception($"RefinanceScenario with ID {request.RefinanceScenarioId} not found.");
        }

        scenario.MortgageId = request.MortgageId;
        scenario.Name = request.Name;
        scenario.NewInterestRate = request.NewInterestRate;
        scenario.NewLoanTermYears = request.NewLoanTermYears;
        scenario.RefinancingCosts = request.RefinancingCosts;
        scenario.NewMonthlyPayment = request.NewMonthlyPayment;
        scenario.MonthlySavings = request.MonthlySavings;
        scenario.BreakEvenMonths = request.BreakEvenMonths;
        scenario.TotalSavings = request.TotalSavings;

        await _context.SaveChangesAsync(cancellationToken);

        return scenario.ToDto();
    }
}
