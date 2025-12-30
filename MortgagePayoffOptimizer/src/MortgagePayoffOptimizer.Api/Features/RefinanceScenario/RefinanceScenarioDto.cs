// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Api.Features.RefinanceScenario;

/// <summary>
/// Data transfer object for RefinanceScenario entity.
/// </summary>
public record RefinanceScenarioDto
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
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Extension methods for mapping RefinanceScenario to RefinanceScenarioDto.
/// </summary>
public static class RefinanceScenarioDtoExtensions
{
    public static RefinanceScenarioDto ToDto(this Core.RefinanceScenario refinanceScenario)
    {
        return new RefinanceScenarioDto
        {
            RefinanceScenarioId = refinanceScenario.RefinanceScenarioId,
            MortgageId = refinanceScenario.MortgageId,
            Name = refinanceScenario.Name,
            NewInterestRate = refinanceScenario.NewInterestRate,
            NewLoanTermYears = refinanceScenario.NewLoanTermYears,
            RefinancingCosts = refinanceScenario.RefinancingCosts,
            NewMonthlyPayment = refinanceScenario.NewMonthlyPayment,
            MonthlySavings = refinanceScenario.MonthlySavings,
            BreakEvenMonths = refinanceScenario.BreakEvenMonths,
            TotalSavings = refinanceScenario.TotalSavings,
            CreatedAt = refinanceScenario.CreatedAt
        };
    }
}
