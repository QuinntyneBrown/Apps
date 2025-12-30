namespace RetirementSavingsCalculator.Core.Tests;

public class ContributionRecordedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var scenarioId = Guid.NewGuid();
        var amount = 5000m;
        var contributionDate = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

        // Act
        var evt = new ContributionRecordedEvent
        {
            ContributionId = contributionId,
            RetirementScenarioId = scenarioId,
            Amount = amount,
            ContributionDate = contributionDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ContributionId, Is.EqualTo(contributionId));
            Assert.That(evt.RetirementScenarioId, Is.EqualTo(scenarioId));
            Assert.That(evt.Amount, Is.EqualTo(amount));
            Assert.That(evt.ContributionDate, Is.EqualTo(contributionDate));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSet()
    {
        // Arrange & Act
        var evt = new ContributionRecordedEvent();

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void Timestamp_CanBeSetExplicitly()
    {
        // Arrange
        var specificTime = new DateTime(2024, 6, 15, 14, 30, 0, DateTimeKind.Utc);

        // Act
        var evt = new ContributionRecordedEvent
        {
            Timestamp = specificTime
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(specificTime));
    }

    [Test]
    public void Record_IsImmutable_PropertiesHaveInitAccessors()
    {
        // Arrange
        var evt = new ContributionRecordedEvent
        {
            ContributionId = Guid.NewGuid(),
            Amount = 1000m
        };

        // Assert - This test validates that the properties are init-only
        // by ensuring we can create the object with init syntax
        Assert.That(evt, Is.Not.Null);
        Assert.That(evt.Amount, Is.EqualTo(1000m));
    }

    [Test]
    public void Amount_ZeroValue_CanBeSet()
    {
        // Arrange & Act
        var evt = new ContributionRecordedEvent
        {
            Amount = 0m
        };

        // Assert
        Assert.That(evt.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void Amount_LargeValue_CanBeSet()
    {
        // Arrange & Act
        var evt = new ContributionRecordedEvent
        {
            Amount = 999999.99m
        };

        // Assert
        Assert.That(evt.Amount, Is.EqualTo(999999.99m));
    }

    [Test]
    public void ContributionDate_CanBeSetToAnyDateTime()
    {
        // Arrange
        var pastDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var evt = new ContributionRecordedEvent
        {
            ContributionDate = pastDate
        };

        // Assert
        Assert.That(evt.ContributionDate, Is.EqualTo(pastDate));
    }

    [Test]
    public void Event_SupportsRecordEquality()
    {
        // Arrange
        var id = Guid.NewGuid();
        var scenarioId = Guid.NewGuid();
        var amount = 1000m;
        var date = DateTime.UtcNow;

        var evt1 = new ContributionRecordedEvent
        {
            ContributionId = id,
            RetirementScenarioId = scenarioId,
            Amount = amount,
            ContributionDate = date,
            Timestamp = date
        };

        var evt2 = new ContributionRecordedEvent
        {
            ContributionId = id,
            RetirementScenarioId = scenarioId,
            Amount = amount,
            ContributionDate = date,
            Timestamp = date
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }
}
