// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core.Tests;

public class EnumTests
{
    [Test]
    public void ActionItemStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var notStarted = ActionItemStatus.NotStarted;
        var inProgress = ActionItemStatus.InProgress;
        var completed = ActionItemStatus.Completed;
        var cancelled = ActionItemStatus.Cancelled;
        var onHold = ActionItemStatus.OnHold;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(notStarted, Is.EqualTo(ActionItemStatus.NotStarted));
            Assert.That(inProgress, Is.EqualTo(ActionItemStatus.InProgress));
            Assert.That(completed, Is.EqualTo(ActionItemStatus.Completed));
            Assert.That(cancelled, Is.EqualTo(ActionItemStatus.Cancelled));
            Assert.That(onHold, Is.EqualTo(ActionItemStatus.OnHold));
        });
    }

    [Test]
    public void ActionItemStatus_NotStartedValue_IsZero()
    {
        // Act
        var value = ActionItemStatus.NotStarted;

        // Assert
        Assert.That((int)value, Is.EqualTo(0));
    }

    [Test]
    public void ActionItemStatus_InProgressValue_IsOne()
    {
        // Act
        var value = ActionItemStatus.InProgress;

        // Assert
        Assert.That((int)value, Is.EqualTo(1));
    }

    [Test]
    public void ActionItemStatus_CompletedValue_IsTwo()
    {
        // Act
        var value = ActionItemStatus.Completed;

        // Assert
        Assert.That((int)value, Is.EqualTo(2));
    }

    [Test]
    public void ActionItemStatus_CancelledValue_IsThree()
    {
        // Act
        var value = ActionItemStatus.Cancelled;

        // Assert
        Assert.That((int)value, Is.EqualTo(3));
    }

    [Test]
    public void ActionItemStatus_OnHoldValue_IsFour()
    {
        // Act
        var value = ActionItemStatus.OnHold;

        // Assert
        Assert.That((int)value, Is.EqualTo(4));
    }

    [Test]
    public void Priority_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var low = Priority.Low;
        var medium = Priority.Medium;
        var high = Priority.High;
        var critical = Priority.Critical;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(low, Is.EqualTo(Priority.Low));
            Assert.That(medium, Is.EqualTo(Priority.Medium));
            Assert.That(high, Is.EqualTo(Priority.High));
            Assert.That(critical, Is.EqualTo(Priority.Critical));
        });
    }

    [Test]
    public void Priority_LowValue_IsZero()
    {
        // Act
        var value = Priority.Low;

        // Assert
        Assert.That((int)value, Is.EqualTo(0));
    }

    [Test]
    public void Priority_MediumValue_IsOne()
    {
        // Act
        var value = Priority.Medium;

        // Assert
        Assert.That((int)value, Is.EqualTo(1));
    }

    [Test]
    public void Priority_HighValue_IsTwo()
    {
        // Act
        var value = Priority.High;

        // Assert
        Assert.That((int)value, Is.EqualTo(2));
    }

    [Test]
    public void Priority_CriticalValue_IsThree()
    {
        // Act
        var value = Priority.Critical;

        // Assert
        Assert.That((int)value, Is.EqualTo(3));
    }

    [Test]
    public void ActionItemStatus_CanBeCompared()
    {
        // Arrange
        var status1 = ActionItemStatus.InProgress;
        var status2 = ActionItemStatus.InProgress;
        var status3 = ActionItemStatus.Completed;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(status1, Is.EqualTo(status2));
            Assert.That(status1, Is.Not.EqualTo(status3));
        });
    }

    [Test]
    public void Priority_CanBeCompared()
    {
        // Arrange
        var priority1 = Priority.High;
        var priority2 = Priority.High;
        var priority3 = Priority.Low;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(priority1, Is.EqualTo(priority2));
            Assert.That(priority1, Is.Not.EqualTo(priority3));
        });
    }

    [Test]
    public void ActionItemStatus_CanBeCastToInt()
    {
        // Arrange
        var status = ActionItemStatus.Completed;

        // Act
        int intValue = (int)status;

        // Assert
        Assert.That(intValue, Is.EqualTo(2));
    }

    [Test]
    public void Priority_CanBeCastToInt()
    {
        // Arrange
        var priority = Priority.Critical;

        // Act
        int intValue = (int)priority;

        // Assert
        Assert.That(intValue, Is.EqualTo(3));
    }

    [Test]
    public void ActionItemStatus_CanBeCastFromInt()
    {
        // Arrange
        int intValue = 1;

        // Act
        var status = (ActionItemStatus)intValue;

        // Assert
        Assert.That(status, Is.EqualTo(ActionItemStatus.InProgress));
    }

    [Test]
    public void Priority_CanBeCastFromInt()
    {
        // Arrange
        int intValue = 2;

        // Act
        var priority = (Priority)intValue;

        // Assert
        Assert.That(priority, Is.EqualTo(Priority.High));
    }
}
