namespace RetirementSavingsCalculator.Core.Tests;

public class WithdrawalStrategyTypeTests
{
    [Test]
    public void FixedAmount_HasCorrectValue()
    {
        // Arrange & Act
        var type = WithdrawalStrategyType.FixedAmount;

        // Assert
        Assert.That((int)type, Is.EqualTo(0));
    }

    [Test]
    public void PercentageBased_HasCorrectValue()
    {
        // Arrange & Act
        var type = WithdrawalStrategyType.PercentageBased;

        // Assert
        Assert.That((int)type, Is.EqualTo(1));
    }

    [Test]
    public void Dynamic_HasCorrectValue()
    {
        // Arrange & Act
        var type = WithdrawalStrategyType.Dynamic;

        // Assert
        Assert.That((int)type, Is.EqualTo(2));
    }

    [Test]
    public void RequiredMinimumDistribution_HasCorrectValue()
    {
        // Arrange & Act
        var type = WithdrawalStrategyType.RequiredMinimumDistribution;

        // Assert
        Assert.That((int)type, Is.EqualTo(3));
    }

    [Test]
    public void FixedAmount_CanBeAssigned()
    {
        // Arrange & Act
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.FixedAmount
        };

        // Assert
        Assert.That(strategy.StrategyType, Is.EqualTo(WithdrawalStrategyType.FixedAmount));
    }

    [Test]
    public void PercentageBased_CanBeAssigned()
    {
        // Arrange & Act
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.PercentageBased
        };

        // Assert
        Assert.That(strategy.StrategyType, Is.EqualTo(WithdrawalStrategyType.PercentageBased));
    }

    [Test]
    public void Dynamic_CanBeAssigned()
    {
        // Arrange & Act
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.Dynamic
        };

        // Assert
        Assert.That(strategy.StrategyType, Is.EqualTo(WithdrawalStrategyType.Dynamic));
    }

    [Test]
    public void RequiredMinimumDistribution_CanBeAssigned()
    {
        // Arrange & Act
        var strategy = new WithdrawalStrategy
        {
            StrategyType = WithdrawalStrategyType.RequiredMinimumDistribution
        };

        // Assert
        Assert.That(strategy.StrategyType, Is.EqualTo(WithdrawalStrategyType.RequiredMinimumDistribution));
    }

    [Test]
    public void AllEnumValues_AreDefined()
    {
        // Arrange & Act
        var values = Enum.GetValues(typeof(WithdrawalStrategyType));

        // Assert
        Assert.That(values.Length, Is.EqualTo(4));
    }

    [Test]
    public void EnumValue_CanBeParsedFromString()
    {
        // Arrange & Act
        var parsed = Enum.Parse<WithdrawalStrategyType>("PercentageBased");

        // Assert
        Assert.That(parsed, Is.EqualTo(WithdrawalStrategyType.PercentageBased));
    }
}
