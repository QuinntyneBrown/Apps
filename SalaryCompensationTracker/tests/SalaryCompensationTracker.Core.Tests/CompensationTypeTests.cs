namespace SalaryCompensationTracker.Core.Tests;

public class CompensationTypeTests
{
    [Test]
    public void FullTime_HasCorrectValue()
    {
        // Arrange & Act
        var type = CompensationType.FullTime;

        // Assert
        Assert.That((int)type, Is.EqualTo(0));
    }

    [Test]
    public void PartTime_HasCorrectValue()
    {
        // Arrange & Act
        var type = CompensationType.PartTime;

        // Assert
        Assert.That((int)type, Is.EqualTo(1));
    }

    [Test]
    public void Contract_HasCorrectValue()
    {
        // Arrange & Act
        var type = CompensationType.Contract;

        // Assert
        Assert.That((int)type, Is.EqualTo(2));
    }

    [Test]
    public void Freelance_HasCorrectValue()
    {
        // Arrange & Act
        var type = CompensationType.Freelance;

        // Assert
        Assert.That((int)type, Is.EqualTo(3));
    }

    [Test]
    public void Consulting_HasCorrectValue()
    {
        // Arrange & Act
        var type = CompensationType.Consulting;

        // Assert
        Assert.That((int)type, Is.EqualTo(4));
    }

    [Test]
    public void Bonus_HasCorrectValue()
    {
        // Arrange & Act
        var type = CompensationType.Bonus;

        // Assert
        Assert.That((int)type, Is.EqualTo(5));
    }

    [Test]
    public void Raise_HasCorrectValue()
    {
        // Arrange & Act
        var type = CompensationType.Raise;

        // Assert
        Assert.That((int)type, Is.EqualTo(6));
    }

    [Test]
    public void Other_HasCorrectValue()
    {
        // Arrange & Act
        var type = CompensationType.Other;

        // Assert
        Assert.That((int)type, Is.EqualTo(7));
    }

    [Test]
    public void FullTime_CanBeAssigned()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationType = CompensationType.FullTime
        };

        // Assert
        Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.FullTime));
    }

    [Test]
    public void PartTime_CanBeAssigned()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationType = CompensationType.PartTime
        };

        // Assert
        Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.PartTime));
    }

    [Test]
    public void Contract_CanBeAssigned()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationType = CompensationType.Contract
        };

        // Assert
        Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.Contract));
    }

    [Test]
    public void Freelance_CanBeAssigned()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationType = CompensationType.Freelance
        };

        // Assert
        Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.Freelance));
    }

    [Test]
    public void Consulting_CanBeAssigned()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationType = CompensationType.Consulting
        };

        // Assert
        Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.Consulting));
    }

    [Test]
    public void Bonus_CanBeAssigned()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationType = CompensationType.Bonus
        };

        // Assert
        Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.Bonus));
    }

    [Test]
    public void Raise_CanBeAssigned()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationType = CompensationType.Raise
        };

        // Assert
        Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.Raise));
    }

    [Test]
    public void Other_CanBeAssigned()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationType = CompensationType.Other
        };

        // Assert
        Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.Other));
    }

    [Test]
    public void AllEnumValues_AreDefined()
    {
        // Arrange & Act
        var values = Enum.GetValues(typeof(CompensationType));

        // Assert
        Assert.That(values.Length, Is.EqualTo(8));
    }

    [Test]
    public void EnumValue_CanBeParsedFromString()
    {
        // Arrange & Act
        var parsed = Enum.Parse<CompensationType>("FullTime");

        // Assert
        Assert.That(parsed, Is.EqualTo(CompensationType.FullTime));
    }
}
