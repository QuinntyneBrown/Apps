namespace RetirementSavingsCalculator.Core.Tests;

public class WithdrawalStrategyTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWithdrawalStrategy()
    {
        // Arrange & Act
        var strategy = new WithdrawalStrategy
        {
            WithdrawalStrategyId = Guid.NewGuid(),
            RetirementScenarioId = Guid.NewGuid(),
            Name = "4% Rule",
            WithdrawalRate = 4m,
            AnnualWithdrawalAmount = 40000m,
            AdjustForInflation = true,
            MinimumBalance = 100000m,
            StrategyType = WithdrawalStrategyType.PercentageBased,
            Notes = "Conservative strategy"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(strategy.WithdrawalStrategyId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(strategy.RetirementScenarioId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(strategy.Name, Is.EqualTo("4% Rule"));
            Assert.That(strategy.WithdrawalRate, Is.EqualTo(4m));
            Assert.That(strategy.AnnualWithdrawalAmount, Is.EqualTo(40000m));
            Assert.That(strategy.AdjustForInflation, Is.True);
            Assert.That(strategy.MinimumBalance, Is.EqualTo(100000m));
            Assert.That(strategy.StrategyType, Is.EqualTo(WithdrawalStrategyType.PercentageBased));
        });
    }

    [Test]
    public void CalculateWithdrawal_PercentageBasedFirstYear_CalculatesCorrectly()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.PercentageBased,
            WithdrawalRate = 4m,
            AdjustForInflation = false
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(1000000m, 1, 3m);

        // Assert
        Assert.That(withdrawal, Is.EqualTo(40000m)); // 4% of 1,000,000
    }

    [Test]
    public void CalculateWithdrawal_FixedAmountFirstYear_ReturnsFixedAmount()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.FixedAmount,
            AnnualWithdrawalAmount = 50000m,
            AdjustForInflation = false
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(800000m, 1, 3m);

        // Assert
        Assert.That(withdrawal, Is.EqualTo(50000m));
    }

    [Test]
    public void CalculateWithdrawal_WithInflationAdjustmentSecondYear_AdjustsForInflation()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.FixedAmount,
            AnnualWithdrawalAmount = 40000m,
            AdjustForInflation = true
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(900000m, 2, 3m);

        // Assert
        // Should be 40000 * (1.03)^1 = 41,200
        Assert.That(withdrawal, Is.EqualTo(41200m).Within(1m));
    }

    [Test]
    public void CalculateWithdrawal_WithInflationAdjustmentFirstYear_NoAdjustment()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.FixedAmount,
            AnnualWithdrawalAmount = 40000m,
            AdjustForInflation = true
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(900000m, 1, 3m);

        // Assert
        Assert.That(withdrawal, Is.EqualTo(40000m)); // Year 1 has no inflation adjustment
    }

    [Test]
    public void CalculateWithdrawal_WithMinimumBalance_LimitsWithdrawal()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.PercentageBased,
            WithdrawalRate = 10m, // High withdrawal rate
            MinimumBalance = 100000m,
            AdjustForInflation = false
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(150000m, 1, 3m);

        // Assert
        // 10% of 150k = 15k, but that would leave only 135k
        // With minimum of 100k, max withdrawal is 50k
        Assert.That(withdrawal, Is.EqualTo(50000m));
    }

    [Test]
    public void CalculateWithdrawal_BalanceAtMinimum_ReturnsZero()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.PercentageBased,
            WithdrawalRate = 4m,
            MinimumBalance = 100000m,
            AdjustForInflation = false
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(100000m, 1, 3m);

        // Assert
        Assert.That(withdrawal, Is.EqualTo(0m));
    }

    [Test]
    public void Validate_ValidWithdrawalRate_DoesNotThrow()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            WithdrawalRate = 4m,
            AnnualWithdrawalAmount = 40000m
        };

        // Act & Assert
        Assert.DoesNotThrow(() => strategy.Validate());
    }

    [Test]
    public void Validate_NegativeWithdrawalRate_ThrowsArgumentException()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            WithdrawalRate = -5m,
            AnnualWithdrawalAmount = 40000m
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => strategy.Validate());
        Assert.That(ex.Message, Does.Contain("Withdrawal rate must be between 0 and 100"));
        Assert.That(ex.ParamName, Is.EqualTo("WithdrawalRate"));
    }

    [Test]
    public void Validate_WithdrawalRateOver100_ThrowsArgumentException()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            WithdrawalRate = 101m,
            AnnualWithdrawalAmount = 40000m
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => strategy.Validate());
        Assert.That(ex.Message, Does.Contain("Withdrawal rate must be between 0 and 100"));
    }

    [Test]
    public void Validate_NegativeAnnualWithdrawalAmount_ThrowsArgumentException()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            WithdrawalRate = 4m,
            AnnualWithdrawalAmount = -1000m
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => strategy.Validate());
        Assert.That(ex.Message, Does.Contain("Annual withdrawal amount cannot be negative"));
        Assert.That(ex.ParamName, Is.EqualTo("AnnualWithdrawalAmount"));
    }

    [Test]
    public void Validate_ZeroValues_DoesNotThrow()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            WithdrawalRate = 0m,
            AnnualWithdrawalAmount = 0m
        };

        // Act & Assert
        Assert.DoesNotThrow(() => strategy.Validate());
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var strategy = new WithdrawalStrategy();

        // Assert
        Assert.That(strategy.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void AdjustForInflation_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var strategy = new WithdrawalStrategy();

        // Assert
        Assert.That(strategy.AdjustForInflation, Is.False);
    }

    [Test]
    public void MinimumBalance_CanBeNull()
    {
        // Arrange & Act
        var strategy = new WithdrawalStrategy
        {
            MinimumBalance = null
        };

        // Assert
        Assert.That(strategy.MinimumBalance, Is.Null);
    }

    [Test]
    public void CalculateWithdrawal_NoMinimumBalance_AllowsFullWithdrawal()
    {
        // Arrange
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.PercentageBased,
            WithdrawalRate = 50m,
            MinimumBalance = null,
            AdjustForInflation = false
        };

        // Act
        var withdrawal = strategy.CalculateWithdrawal(100000m, 1, 3m);

        // Assert
        Assert.That(withdrawal, Is.EqualTo(50000m)); // 50% of balance
    }

    [Test]
    public void RetirementScenario_NavigationProperty_CanBeSet()
    {
        // Arrange
        var strategy = new WithdrawalStrategy();
        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Test Scenario"
        };

        // Act
        strategy.RetirementScenario = scenario;

        // Assert
        Assert.That(strategy.RetirementScenario, Is.Not.Null);
        Assert.That(strategy.RetirementScenario.Name, Is.EqualTo("Test Scenario"));
    }
}
