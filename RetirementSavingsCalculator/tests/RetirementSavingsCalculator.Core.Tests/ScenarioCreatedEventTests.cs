namespace RetirementSavingsCalculator.Core.Tests;

public class ScenarioCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var scenarioId = Guid.NewGuid();
        var name = "Aggressive Growth Plan";
        var currentAge = 30;
        var retirementAge = 65;

        // Act
        var evt = new ScenarioCreatedEvent
        {
            RetirementScenarioId = scenarioId,
            Name = name,
            CurrentAge = currentAge,
            RetirementAge = retirementAge
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RetirementScenarioId, Is.EqualTo(scenarioId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.CurrentAge, Is.EqualTo(currentAge));
            Assert.That(evt.RetirementAge, Is.EqualTo(retirementAge));
        });
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var evt = new ScenarioCreatedEvent();

        // Assert
        Assert.That(evt.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Timestamp_DefaultValue_IsSet()
    {
        // Arrange & Act
        var evt = new ScenarioCreatedEvent();

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void Timestamp_CanBeSetExplicitly()
    {
        // Arrange
        var specificTime = new DateTime(2024, 3, 20, 9, 15, 0, DateTimeKind.Utc);

        // Act
        var evt = new ScenarioCreatedEvent
        {
            Timestamp = specificTime
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(specificTime));
    }

    [Test]
    public void CurrentAge_CanBeSetToAnyValue()
    {
        // Arrange & Act
        var evt = new ScenarioCreatedEvent
        {
            CurrentAge = 25
        };

        // Assert
        Assert.That(evt.CurrentAge, Is.EqualTo(25));
    }

    [Test]
    public void RetirementAge_CanBeSetToAnyValue()
    {
        // Arrange & Act
        var evt = new ScenarioCreatedEvent
        {
            RetirementAge = 70
        };

        // Assert
        Assert.That(evt.RetirementAge, Is.EqualTo(70));
    }

    [Test]
    public void Event_SupportsRecordEquality()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Test Plan";
        var currentAge = 30;
        var retirementAge = 65;
        var timestamp = DateTime.UtcNow;

        var evt1 = new ScenarioCreatedEvent
        {
            RetirementScenarioId = id,
            Name = name,
            CurrentAge = currentAge,
            RetirementAge = retirementAge,
            Timestamp = timestamp
        };

        var evt2 = new ScenarioCreatedEvent
        {
            RetirementScenarioId = id,
            Name = name,
            CurrentAge = currentAge,
            RetirementAge = retirementAge,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void Event_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new ScenarioCreatedEvent
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Plan A",
            CurrentAge = 30,
            RetirementAge = 65
        };

        var evt2 = new ScenarioCreatedEvent
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Plan B",
            CurrentAge = 35,
            RetirementAge = 67
        };

        // Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
