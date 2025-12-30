namespace CouplesGoalTracker.Core.Tests;

public class GoalCategoryTests
{
    [Test]
    public void GoalCategory_Communication_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.Communication;

        // Assert
        Assert.That((int)category, Is.EqualTo(0));
    }

    [Test]
    public void GoalCategory_Intimacy_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.Intimacy;

        // Assert
        Assert.That((int)category, Is.EqualTo(1));
    }

    [Test]
    public void GoalCategory_Financial_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.Financial;

        // Assert
        Assert.That((int)category, Is.EqualTo(2));
    }

    [Test]
    public void GoalCategory_HealthAndWellness_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.HealthAndWellness;

        // Assert
        Assert.That((int)category, Is.EqualTo(3));
    }

    [Test]
    public void GoalCategory_AdventureAndTravel_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.AdventureAndTravel;

        // Assert
        Assert.That((int)category, Is.EqualTo(4));
    }

    [Test]
    public void GoalCategory_PersonalGrowth_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.PersonalGrowth;

        // Assert
        Assert.That((int)category, Is.EqualTo(5));
    }

    [Test]
    public void GoalCategory_FamilyPlanning_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.FamilyPlanning;

        // Assert
        Assert.That((int)category, Is.EqualTo(6));
    }

    [Test]
    public void GoalCategory_QualityTime_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.QualityTime;

        // Assert
        Assert.That((int)category, Is.EqualTo(7));
    }

    [Test]
    public void GoalCategory_CareerAndEducation_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.CareerAndEducation;

        // Assert
        Assert.That((int)category, Is.EqualTo(8));
    }

    [Test]
    public void GoalCategory_Other_HasCorrectValue()
    {
        // Arrange & Act
        var category = GoalCategory.Other;

        // Assert
        Assert.That((int)category, Is.EqualTo(9));
    }

    [Test]
    public void GoalCategory_CanBeAssignedToProperty()
    {
        // Arrange
        var goal = new Goal();

        // Act
        goal.Category = GoalCategory.Financial;

        // Assert
        Assert.That(goal.Category, Is.EqualTo(GoalCategory.Financial));
    }

    [Test]
    public void GoalCategory_AllValuesAreUnique()
    {
        // Arrange
        var values = Enum.GetValues<GoalCategory>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }
}
