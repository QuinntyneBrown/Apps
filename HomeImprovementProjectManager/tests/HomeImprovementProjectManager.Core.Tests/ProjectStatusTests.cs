// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core.Tests;

public class ProjectStatusTests
{
    [Test]
    public void ProjectStatus_Planning_HasCorrectValue()
    {
        // Arrange & Act
        var status = ProjectStatus.Planning;

        // Assert
        Assert.That((int)status, Is.EqualTo(0));
    }

    [Test]
    public void ProjectStatus_InProgress_HasCorrectValue()
    {
        // Arrange & Act
        var status = ProjectStatus.InProgress;

        // Assert
        Assert.That((int)status, Is.EqualTo(1));
    }

    [Test]
    public void ProjectStatus_Completed_HasCorrectValue()
    {
        // Arrange & Act
        var status = ProjectStatus.Completed;

        // Assert
        Assert.That((int)status, Is.EqualTo(2));
    }

    [Test]
    public void ProjectStatus_OnHold_HasCorrectValue()
    {
        // Arrange & Act
        var status = ProjectStatus.OnHold;

        // Assert
        Assert.That((int)status, Is.EqualTo(3));
    }

    [Test]
    public void ProjectStatus_Cancelled_HasCorrectValue()
    {
        // Arrange & Act
        var status = ProjectStatus.Cancelled;

        // Assert
        Assert.That((int)status, Is.EqualTo(4));
    }

    [Test]
    public void ProjectStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var planning = ProjectStatus.Planning;
        var inProgress = ProjectStatus.InProgress;
        var completed = ProjectStatus.Completed;
        var onHold = ProjectStatus.OnHold;
        var cancelled = ProjectStatus.Cancelled;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(planning, Is.EqualTo(ProjectStatus.Planning));
            Assert.That(inProgress, Is.EqualTo(ProjectStatus.InProgress));
            Assert.That(completed, Is.EqualTo(ProjectStatus.Completed));
            Assert.That(onHold, Is.EqualTo(ProjectStatus.OnHold));
            Assert.That(cancelled, Is.EqualTo(ProjectStatus.Cancelled));
        });
    }

    [Test]
    public void ProjectStatus_ToString_ReturnsCorrectName()
    {
        // Arrange & Act
        var planningName = ProjectStatus.Planning.ToString();
        var inProgressName = ProjectStatus.InProgress.ToString();
        var completedName = ProjectStatus.Completed.ToString();
        var onHoldName = ProjectStatus.OnHold.ToString();
        var cancelledName = ProjectStatus.Cancelled.ToString();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(planningName, Is.EqualTo("Planning"));
            Assert.That(inProgressName, Is.EqualTo("InProgress"));
            Assert.That(completedName, Is.EqualTo("Completed"));
            Assert.That(onHoldName, Is.EqualTo("OnHold"));
            Assert.That(cancelledName, Is.EqualTo("Cancelled"));
        });
    }

    [Test]
    public void ProjectStatus_CanBeCompared()
    {
        // Arrange
        var status1 = ProjectStatus.Planning;
        var status2 = ProjectStatus.Planning;
        var status3 = ProjectStatus.InProgress;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(status1, Is.EqualTo(status2));
            Assert.That(status1, Is.Not.EqualTo(status3));
        });
    }

    [Test]
    public void ProjectStatus_CanBeUsedInSwitch()
    {
        // Arrange
        var status = ProjectStatus.Completed;
        string result;

        // Act
        result = status switch
        {
            ProjectStatus.Planning => "Planning",
            ProjectStatus.InProgress => "In Progress",
            ProjectStatus.Completed => "Completed",
            ProjectStatus.OnHold => "On Hold",
            ProjectStatus.Cancelled => "Cancelled",
            _ => "Unknown"
        };

        // Assert
        Assert.That(result, Is.EqualTo("Completed"));
    }

    [Test]
    public void ProjectStatus_EnumParse_WorksCorrectly()
    {
        // Arrange
        var statusName = "InProgress";

        // Act
        var parsed = Enum.Parse<ProjectStatus>(statusName);

        // Assert
        Assert.That(parsed, Is.EqualTo(ProjectStatus.InProgress));
    }
}
