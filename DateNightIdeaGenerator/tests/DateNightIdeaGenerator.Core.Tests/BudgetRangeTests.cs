namespace DateNightIdeaGenerator.Core.Tests;

public class BudgetRangeTests
{
    [Test]
    public void BudgetRange_Free_HasCorrectValue()
    {
        // Arrange & Act
        var budget = BudgetRange.Free;

        // Assert
        Assert.That((int)budget, Is.EqualTo(0));
    }

    [Test]
    public void BudgetRange_Low_HasCorrectValue()
    {
        // Arrange & Act
        var budget = BudgetRange.Low;

        // Assert
        Assert.That((int)budget, Is.EqualTo(1));
    }

    [Test]
    public void BudgetRange_Medium_HasCorrectValue()
    {
        // Arrange & Act
        var budget = BudgetRange.Medium;

        // Assert
        Assert.That((int)budget, Is.EqualTo(2));
    }

    [Test]
    public void BudgetRange_High_HasCorrectValue()
    {
        // Arrange & Act
        var budget = BudgetRange.High;

        // Assert
        Assert.That((int)budget, Is.EqualTo(3));
    }

    [Test]
    public void BudgetRange_Premium_HasCorrectValue()
    {
        // Arrange & Act
        var budget = BudgetRange.Premium;

        // Assert
        Assert.That((int)budget, Is.EqualTo(4));
    }

    [Test]
    public void BudgetRange_CanBeAssignedToProperty()
    {
        // Arrange
        var dateIdea = new DateIdea();

        // Act
        dateIdea.BudgetRange = BudgetRange.Medium;

        // Assert
        Assert.That(dateIdea.BudgetRange, Is.EqualTo(BudgetRange.Medium));
    }

    [Test]
    public void BudgetRange_AllValuesAreUnique()
    {
        // Arrange
        var values = Enum.GetValues<BudgetRange>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }
}
