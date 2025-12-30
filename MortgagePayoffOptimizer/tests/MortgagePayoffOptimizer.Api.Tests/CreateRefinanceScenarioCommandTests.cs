// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Api.Tests;

/// <summary>
/// Tests for CreateRefinanceScenarioCommand.
/// </summary>
[TestFixture]
public class CreateRefinanceScenarioCommandTests
{
    private Mock<IMortgagePayoffOptimizerContext> _mockContext;
    private List<RefinanceScenario> _scenarios;

    [SetUp]
    public void Setup()
    {
        _scenarios = new List<RefinanceScenario>();
        _mockContext = new Mock<IMortgagePayoffOptimizerContext>();

        var mockSet = TestHelpers.CreateMockDbSet(_scenarios);
        _mockContext.Setup(c => c.RefinanceScenarios).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
    }

    [Test]
    public async Task Handle_ShouldCreateRefinanceScenario()
    {
        // Arrange
        var handler = new CreateRefinanceScenarioCommandHandler(_mockContext.Object);
        var command = new CreateRefinanceScenarioCommand
        {
            MortgageId = Guid.NewGuid(),
            Name = "Lower Rate Scenario",
            NewInterestRate = 2.75m,
            NewLoanTermYears = 30,
            RefinancingCosts = 5000m,
            NewMonthlyPayment = 1020.50m,
            MonthlySavings = 326.63m,
            BreakEvenMonths = 15,
            TotalSavings = 117588m
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.MortgageId, Is.EqualTo(command.MortgageId));
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.NewInterestRate, Is.EqualTo(command.NewInterestRate));
        Assert.That(_scenarios, Has.Count.EqualTo(1));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
