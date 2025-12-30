using RetirementSavingsCalculator.Api.Features.RetirementScenarios;
using RetirementSavingsCalculator.Api.Features.Contributions;
using RetirementSavingsCalculator.Api.Features.WithdrawalStrategies;

namespace RetirementSavingsCalculator.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void RetirementScenarioDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var scenario = new Core.RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Test Scenario",
            CurrentAge = 35,
            RetirementAge = 65,
            LifeExpectancyAge = 90,
            CurrentSavings = 100000m,
            AnnualContribution = 18000m,
            ExpectedReturnRate = 6.5m,
            InflationRate = 2.5m,
            ProjectedAnnualIncome = 25000m,
            ProjectedAnnualExpenses = 60000m,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        // Act
        var dto = scenario.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.RetirementScenarioId, Is.EqualTo(scenario.RetirementScenarioId));
            Assert.That(dto.Name, Is.EqualTo(scenario.Name));
            Assert.That(dto.CurrentAge, Is.EqualTo(scenario.CurrentAge));
            Assert.That(dto.RetirementAge, Is.EqualTo(scenario.RetirementAge));
            Assert.That(dto.LifeExpectancyAge, Is.EqualTo(scenario.LifeExpectancyAge));
            Assert.That(dto.CurrentSavings, Is.EqualTo(scenario.CurrentSavings));
            Assert.That(dto.AnnualContribution, Is.EqualTo(scenario.AnnualContribution));
            Assert.That(dto.ExpectedReturnRate, Is.EqualTo(scenario.ExpectedReturnRate));
            Assert.That(dto.InflationRate, Is.EqualTo(scenario.InflationRate));
            Assert.That(dto.ProjectedAnnualIncome, Is.EqualTo(scenario.ProjectedAnnualIncome));
            Assert.That(dto.ProjectedAnnualExpenses, Is.EqualTo(scenario.ProjectedAnnualExpenses));
            Assert.That(dto.Notes, Is.EqualTo(scenario.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(scenario.CreatedAt));
            Assert.That(dto.LastUpdated, Is.EqualTo(scenario.LastUpdated));
            Assert.That(dto.ProjectedSavings, Is.EqualTo(scenario.CalculateProjectedSavings()));
            Assert.That(dto.AnnualWithdrawalNeeded, Is.EqualTo(scenario.CalculateAnnualWithdrawal()));
            Assert.That(dto.YearsToRetirement, Is.EqualTo(30));
            Assert.That(dto.YearsInRetirement, Is.EqualTo(25));
        });
    }

    [Test]
    public void ContributionDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var contribution = new Core.Contribution
        {
            ContributionId = Guid.NewGuid(),
            RetirementScenarioId = Guid.NewGuid(),
            Amount = 1500m,
            ContributionDate = DateTime.UtcNow,
            AccountName = "401(k)",
            IsEmployerMatch = true,
            Notes = "Test contribution",
        };

        // Act
        var dto = contribution.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ContributionId, Is.EqualTo(contribution.ContributionId));
            Assert.That(dto.RetirementScenarioId, Is.EqualTo(contribution.RetirementScenarioId));
            Assert.That(dto.Amount, Is.EqualTo(contribution.Amount));
            Assert.That(dto.ContributionDate, Is.EqualTo(contribution.ContributionDate));
            Assert.That(dto.AccountName, Is.EqualTo(contribution.AccountName));
            Assert.That(dto.IsEmployerMatch, Is.EqualTo(contribution.IsEmployerMatch));
            Assert.That(dto.Notes, Is.EqualTo(contribution.Notes));
        });
    }

    [Test]
    public void WithdrawalStrategyDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var strategy = new Core.WithdrawalStrategy
        {
            WithdrawalStrategyId = Guid.NewGuid(),
            RetirementScenarioId = Guid.NewGuid(),
            Name = "4% Rule",
            WithdrawalRate = 4m,
            AnnualWithdrawalAmount = 40000m,
            AdjustForInflation = true,
            MinimumBalance = 100000m,
            StrategyType = Core.WithdrawalStrategyType.PercentageBased,
            Notes = "Test strategy",
        };

        // Act
        var dto = strategy.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.WithdrawalStrategyId, Is.EqualTo(strategy.WithdrawalStrategyId));
            Assert.That(dto.RetirementScenarioId, Is.EqualTo(strategy.RetirementScenarioId));
            Assert.That(dto.Name, Is.EqualTo(strategy.Name));
            Assert.That(dto.WithdrawalRate, Is.EqualTo(strategy.WithdrawalRate));
            Assert.That(dto.AnnualWithdrawalAmount, Is.EqualTo(strategy.AnnualWithdrawalAmount));
            Assert.That(dto.AdjustForInflation, Is.EqualTo(strategy.AdjustForInflation));
            Assert.That(dto.MinimumBalance, Is.EqualTo(strategy.MinimumBalance));
            Assert.That(dto.StrategyType, Is.EqualTo(strategy.StrategyType));
            Assert.That(dto.Notes, Is.EqualTo(strategy.Notes));
        });
    }

    [Test]
    public void RetirementScenario_CalculateProjectedSavings_ReturnsCorrectValue()
    {
        // Arrange
        var scenario = new Core.RetirementScenario
        {
            CurrentAge = 30,
            RetirementAge = 40,
            CurrentSavings = 100000m,
            AnnualContribution = 12000m,
            ExpectedReturnRate = 6m,
        };

        // Act
        var projectedSavings = scenario.CalculateProjectedSavings();

        // Assert
        Assert.That(projectedSavings, Is.GreaterThan(100000m));
    }

    [Test]
    public void RetirementScenario_CalculateAnnualWithdrawal_ReturnsCorrectValue()
    {
        // Arrange
        var scenario = new Core.RetirementScenario
        {
            ProjectedAnnualIncome = 30000m,
            ProjectedAnnualExpenses = 75000m,
        };

        // Act
        var annualWithdrawal = scenario.CalculateAnnualWithdrawal();

        // Assert
        Assert.That(annualWithdrawal, Is.EqualTo(45000m));
    }

    [Test]
    public void WithdrawalStrategy_CalculateWithdrawal_PercentageBased_ReturnsCorrectValue()
    {
        // Arrange
        var strategy = new Core.WithdrawalStrategy
        {
            StrategyType = Core.WithdrawalStrategyType.PercentageBased,
            WithdrawalRate = 4m,
            AdjustForInflation = false,
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(1000000m, 1, 2.5m);

        // Assert
        Assert.That(withdrawal, Is.EqualTo(40000m));
    }

    [Test]
    public void WithdrawalStrategy_CalculateWithdrawal_FixedAmount_ReturnsCorrectValue()
    {
        // Arrange
        var strategy = new Core.WithdrawalStrategy
        {
            StrategyType = Core.WithdrawalStrategyType.FixedAmount,
            AnnualWithdrawalAmount = 50000m,
            AdjustForInflation = false,
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(1000000m, 1, 2.5m);

        // Assert
        Assert.That(withdrawal, Is.EqualTo(50000m));
    }

    [Test]
    public void Contribution_ValidateAmount_ThrowsException_WhenAmountIsZero()
    {
        // Arrange
        var contribution = new Core.Contribution
        {
            Amount = 0m,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => contribution.ValidateAmount());
    }

    [Test]
    public void Contribution_ValidateAmount_ThrowsException_WhenAmountIsNegative()
    {
        // Arrange
        var contribution = new Core.Contribution
        {
            Amount = -100m,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => contribution.ValidateAmount());
    }

    [Test]
    public void WithdrawalStrategy_Validate_ThrowsException_WhenWithdrawalRateIsInvalid()
    {
        // Arrange
        var strategy = new Core.WithdrawalStrategy
        {
            WithdrawalRate = 150m,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => strategy.Validate());
    }
}
