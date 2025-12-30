namespace CouplesGoalTracker.Core.Tests;

public class GoalStatusTests
{
    [Test]
    public void GoalStatus_NotStarted_HasCorrectValue()
    {
        // Arrange & Act
        var status = GoalStatus.NotStarted;

        // Assert
        Assert.That((int)status, Is.EqualTo(0));
    }

    [Test]
    public void GoalStatus_InProgress_HasCorrectValue()
    {
        // Arrange & Act
        var status = GoalStatus.InProgress;

        // Assert
        Assert.That((int)status, Is.EqualTo(1));
    }

    [Test]
    public void GoalStatus_Completed_HasCorrectValue()
    {
        // Arrange & Act
        var status = GoalStatus.Completed;

        // Assert
        Assert.That((int)status, Is.EqualTo(2));
    }

    [Test]
    public void GoalStatus_OnHold_HasCorrectValue()
    {
        // Arrange & Act
        var status = GoalStatus.OnHold;

        // Assert
        Assert.That((int)status, Is.EqualTo(3));
    }

    [Test]
    public void GoalStatus_Cancelled_HasCorrectValue()
    {
        // Arrange & Act
        var status = GoalStatus.Cancelled;

        // Assert
        Assert.That((int)status, Is.EqualTo(4));
    }

    [Test]
    public void GoalStatus_CanBeAssignedToProperty()
    {
        // Arrange
        var goal = new Goal();

        // Act
        goal.Status = GoalStatus.InProgress;

        // Assert
        Assert.That(goal.Status, Is.EqualTo(GoalStatus.InProgress));
    }

    [Test]
    public void GoalStatus_AllValuesAreUnique()
    {
        // Arrange
        var values = Enum.GetValues<GoalStatus>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }
}
