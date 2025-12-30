// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Api.Tests;

/// <summary>
/// Tests for RefinanceScenario DTO mapping.
/// </summary>
[TestFixture]
public class RefinanceScenarioDtoMappingTests
{
    [Test]
    public void ToDto_ShouldMapRefinanceScenarioToRefinanceScenarioDto()
    {
        // Arrange
        var scenario = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            Name = "Lower Rate Scenario",
            NewInterestRate = 2.75m,
            NewLoanTermYears = 30,
            RefinancingCosts = 5000m,
            NewMonthlyPayment = 1020.50m,
            MonthlySavings = 326.63m,
            BreakEvenMonths = 15,
            TotalSavings = 117588m,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = scenario.ToDto();

        // Assert
        Assert.That(dto.RefinanceScenarioId, Is.EqualTo(scenario.RefinanceScenarioId));
        Assert.That(dto.MortgageId, Is.EqualTo(scenario.MortgageId));
        Assert.That(dto.Name, Is.EqualTo(scenario.Name));
        Assert.That(dto.NewInterestRate, Is.EqualTo(scenario.NewInterestRate));
        Assert.That(dto.NewLoanTermYears, Is.EqualTo(scenario.NewLoanTermYears));
        Assert.That(dto.RefinancingCosts, Is.EqualTo(scenario.RefinancingCosts));
        Assert.That(dto.NewMonthlyPayment, Is.EqualTo(scenario.NewMonthlyPayment));
        Assert.That(dto.MonthlySavings, Is.EqualTo(scenario.MonthlySavings));
        Assert.That(dto.BreakEvenMonths, Is.EqualTo(scenario.BreakEvenMonths));
        Assert.That(dto.TotalSavings, Is.EqualTo(scenario.TotalSavings));
        Assert.That(dto.CreatedAt, Is.EqualTo(scenario.CreatedAt));
    }
}
