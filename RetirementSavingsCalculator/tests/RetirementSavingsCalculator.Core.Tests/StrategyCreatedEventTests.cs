namespace RetirementSavingsCalculator.Core.Tests;

public class StrategyCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var strategyId = Guid.NewGuid();
        var scenarioId = Guid.NewGuid();
        var name = "4% Rule Strategy";
        var strategyType = WithdrawalStrategyType.PercentageBased;

        // Act
        var evt = new StrategyCreatedEvent
        {
            WithdrawalStrategyId = strategyId,
            RetirementScenarioId = scenarioId,
            Name = name,
            StrategyType = strategyType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.WithdrawalStrategyId, Is.EqualTo(strategyId));
            Assert.That(evt.RetirementScenarioId, Is.EqualTo(scenarioId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.StrategyType, Is.EqualTo(strategyType));
        });
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var evt = new StrategyCreatedEvent();

        // Assert
        Assert.That(evt.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Timestamp_DefaultValue_IsSet()
    {
        // Arrange & Act
        var evt = new StrategyCreatedEvent();

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void Timestamp_CanBeSetExplicitly()
    {
        // Arrange
        var specificTime = new DateTime(2024, 5, 10, 16, 45, 0, DateTimeKind.Utc);

        // Act
        var evt = new StrategyCreatedEvent
        {
            Timestamp = specificTime
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(specificTime));
    }

    [Test]
    public void StrategyType_FixedAmount_CanBeSet()
    {
        // Arrange & Act
        var evt = new StrategyCreatedEvent
        {
            StrategyType = WithdrawalStrategyType.FixedAmount
        };

        // Assert
        Assert.That(evt.StrategyType, Is.EqualTo(WithdrawalStrategyType.FixedAmount));
    }

    [Test]
    public void StrategyType_PercentageBased_CanBeSet()
    {
        // Arrange & Act
        var evt = new StrategyCreatedEvent
        {
            StrategyType = WithdrawalStrategyType.PercentageBased
        };

        // Assert
        Assert.That(evt.StrategyType, Is.EqualTo(WithdrawalStrategyType.PercentageBased));
    }

    [Test]
    public void StrategyType_Dynamic_CanBeSet()
    {
        // Arrange & Act
        var evt = new StrategyCreatedEvent
        {
            StrategyType = WithdrawalStrategyType.Dynamic
        };

        // Assert
        Assert.That(evt.StrategyType, Is.EqualTo(WithdrawalStrategyType.Dynamic));
    }

    [Test]
    public void StrategyType_RequiredMinimumDistribution_CanBeSet()
    {
        // Arrange & Act
        var evt = new StrategyCreatedEvent
        {
            StrategyType = WithdrawalStrategyType.RequiredMinimumDistribution
        };

        // Assert
        Assert.That(evt.StrategyType, Is.EqualTo(WithdrawalStrategyType.RequiredMinimumDistribution));
    }

    [Test]
    public void Event_SupportsRecordEquality()
    {
        // Arrange
        var strategyId = Guid.NewGuid();
        var scenarioId = Guid.NewGuid();
        var name = "Test Strategy";
        var type = WithdrawalStrategyType.PercentageBased;
        var timestamp = DateTime.UtcNow;

        var evt1 = new StrategyCreatedEvent
        {
            WithdrawalStrategyId = strategyId,
            RetirementScenarioId = scenarioId,
            Name = name,
            StrategyType = type,
            Timestamp = timestamp
        };

        var evt2 = new StrategyCreatedEvent
        {
            WithdrawalStrategyId = strategyId,
            RetirementScenarioId = scenarioId,
            Name = name,
            StrategyType = type,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void Event_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new StrategyCreatedEvent
        {
            WithdrawalStrategyId = Guid.NewGuid(),
            Name = "Strategy A",
            StrategyType = WithdrawalStrategyType.FixedAmount
        };

        var evt2 = new StrategyCreatedEvent
        {
            WithdrawalStrategyId = Guid.NewGuid(),
            Name = "Strategy B",
            StrategyType = WithdrawalStrategyType.PercentageBased
        };

        // Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
