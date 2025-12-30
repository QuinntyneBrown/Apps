namespace RunningLogRaceTracker.Core.Tests;

public class RaceTypeTests
{
    [Test]
    public void FiveK_HasCorrectValue()
    {
        // Arrange & Act
        var type = RaceType.FiveK;

        // Assert
        Assert.That((int)type, Is.EqualTo(0));
    }

    [Test]
    public void TenK_HasCorrectValue()
    {
        // Arrange & Act
        var type = RaceType.TenK;

        // Assert
        Assert.That((int)type, Is.EqualTo(1));
    }

    [Test]
    public void HalfMarathon_HasCorrectValue()
    {
        // Arrange & Act
        var type = RaceType.HalfMarathon;

        // Assert
        Assert.That((int)type, Is.EqualTo(2));
    }

    [Test]
    public void Marathon_HasCorrectValue()
    {
        // Arrange & Act
        var type = RaceType.Marathon;

        // Assert
        Assert.That((int)type, Is.EqualTo(3));
    }

    [Test]
    public void UltraMarathon_HasCorrectValue()
    {
        // Arrange & Act
        var type = RaceType.UltraMarathon;

        // Assert
        Assert.That((int)type, Is.EqualTo(4));
    }

    [Test]
    public void Trail_HasCorrectValue()
    {
        // Arrange & Act
        var type = RaceType.Trail;

        // Assert
        Assert.That((int)type, Is.EqualTo(5));
    }

    [Test]
    public void Other_HasCorrectValue()
    {
        // Arrange & Act
        var type = RaceType.Other;

        // Assert
        Assert.That((int)type, Is.EqualTo(6));
    }

    [Test]
    public void FiveK_CanBeAssigned()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceType = RaceType.FiveK
        };

        // Assert
        Assert.That(race.RaceType, Is.EqualTo(RaceType.FiveK));
    }

    [Test]
    public void TenK_CanBeAssigned()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceType = RaceType.TenK
        };

        // Assert
        Assert.That(race.RaceType, Is.EqualTo(RaceType.TenK));
    }

    [Test]
    public void HalfMarathon_CanBeAssigned()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceType = RaceType.HalfMarathon
        };

        // Assert
        Assert.That(race.RaceType, Is.EqualTo(RaceType.HalfMarathon));
    }

    [Test]
    public void Marathon_CanBeAssigned()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceType = RaceType.Marathon
        };

        // Assert
        Assert.That(race.RaceType, Is.EqualTo(RaceType.Marathon));
    }

    [Test]
    public void UltraMarathon_CanBeAssigned()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceType = RaceType.UltraMarathon
        };

        // Assert
        Assert.That(race.RaceType, Is.EqualTo(RaceType.UltraMarathon));
    }

    [Test]
    public void Trail_CanBeAssigned()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceType = RaceType.Trail
        };

        // Assert
        Assert.That(race.RaceType, Is.EqualTo(RaceType.Trail));
    }

    [Test]
    public void Other_CanBeAssigned()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceType = RaceType.Other
        };

        // Assert
        Assert.That(race.RaceType, Is.EqualTo(RaceType.Other));
    }

    [Test]
    public void AllEnumValues_AreDefined()
    {
        // Arrange & Act
        var values = Enum.GetValues(typeof(RaceType));

        // Assert
        Assert.That(values.Length, Is.EqualTo(7));
    }

    [Test]
    public void EnumValue_CanBeParsedFromString()
    {
        // Arrange & Act
        var parsed = Enum.Parse<RaceType>("Marathon");

        // Assert
        Assert.That(parsed, Is.EqualTo(RaceType.Marathon));
    }
}
