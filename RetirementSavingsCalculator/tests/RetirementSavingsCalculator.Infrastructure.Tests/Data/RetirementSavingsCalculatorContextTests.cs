// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RetirementSavingsCalculator.Infrastructure.Tests;

/// <summary>
/// Unit tests for the RetirementSavingsCalculatorContext.
/// </summary>
[TestFixture]
public class RetirementSavingsCalculatorContextTests
{
    private RetirementSavingsCalculatorContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RetirementSavingsCalculatorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new RetirementSavingsCalculatorContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that RetirementScenarios can be added and retrieved.
    /// </summary>
    [Test]
    public async Task RetirementScenarios_CanAddAndRetrieve()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Test Scenario",
            CurrentAge = 35,
            RetirementAge = 65,
            LifeExpectancyAge = 90,
            CurrentSavings = 100000m,
            AnnualContribution = 10000m,
            ExpectedReturnRate = 6m,
            InflationRate = 2.5m,
            ProjectedAnnualIncome = 20000m,
            ProjectedAnnualExpenses = 50000m,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        // Act
        _context.RetirementScenarios.Add(scenario);
        await _context.SaveChangesAsync();

        var retrieved = await _context.RetirementScenarios.FindAsync(scenario.RetirementScenarioId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Scenario"));
        Assert.That(retrieved.CurrentAge, Is.EqualTo(35));
    }

    /// <summary>
    /// Tests that Contributions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Contributions_CanAddAndRetrieve()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Test Scenario",
            CurrentAge = 35,
            RetirementAge = 65,
            LifeExpectancyAge = 90,
            CurrentSavings = 100000m,
            AnnualContribution = 10000m,
            ExpectedReturnRate = 6m,
            InflationRate = 2.5m,
            ProjectedAnnualIncome = 20000m,
            ProjectedAnnualExpenses = 50000m,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            RetirementScenarioId = scenario.RetirementScenarioId,
            Amount = 1000m,
            ContributionDate = DateTime.UtcNow,
            AccountName = "401(k)",
            IsEmployerMatch = false,
        };

        // Act
        _context.RetirementScenarios.Add(scenario);
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Contributions.FindAsync(contribution.ContributionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Amount, Is.EqualTo(1000m));
        Assert.That(retrieved.AccountName, Is.EqualTo("401(k)"));
    }

    /// <summary>
    /// Tests that WithdrawalStrategies can be added and retrieved.
    /// </summary>
    [Test]
    public async Task WithdrawalStrategies_CanAddAndRetrieve()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Test Scenario",
            CurrentAge = 35,
            RetirementAge = 65,
            LifeExpectancyAge = 90,
            CurrentSavings = 100000m,
            AnnualContribution = 10000m,
            ExpectedReturnRate = 6m,
            InflationRate = 2.5m,
            ProjectedAnnualIncome = 20000m,
            ProjectedAnnualExpenses = 50000m,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        var strategy = new WithdrawalStrategy
        {
            WithdrawalStrategyId = Guid.NewGuid(),
            RetirementScenarioId = scenario.RetirementScenarioId,
            Name = "4% Rule",
            WithdrawalRate = 4m,
            AnnualWithdrawalAmount = 0m,
            AdjustForInflation = true,
            StrategyType = WithdrawalStrategyType.PercentageBased,
        };

        // Act
        _context.RetirementScenarios.Add(scenario);
        _context.WithdrawalStrategies.Add(strategy);
        await _context.SaveChangesAsync();

        var retrieved = await _context.WithdrawalStrategies.FindAsync(strategy.WithdrawalStrategyId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("4% Rule"));
        Assert.That(retrieved.WithdrawalRate, Is.EqualTo(4m));
    }

    /// <summary>
    /// Tests that cascade delete works for contributions when scenario is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesContributions()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Test Scenario",
            CurrentAge = 35,
            RetirementAge = 65,
            LifeExpectancyAge = 90,
            CurrentSavings = 100000m,
            AnnualContribution = 10000m,
            ExpectedReturnRate = 6m,
            InflationRate = 2.5m,
            ProjectedAnnualIncome = 20000m,
            ProjectedAnnualExpenses = 50000m,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            RetirementScenarioId = scenario.RetirementScenarioId,
            Amount = 1000m,
            ContributionDate = DateTime.UtcNow,
            AccountName = "401(k)",
            IsEmployerMatch = false,
        };

        _context.RetirementScenarios.Add(scenario);
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();

        // Act
        _context.RetirementScenarios.Remove(scenario);
        await _context.SaveChangesAsync();

        var retrievedContribution = await _context.Contributions.FindAsync(contribution.ContributionId);

        // Assert
        Assert.That(retrievedContribution, Is.Null);
    }
}
