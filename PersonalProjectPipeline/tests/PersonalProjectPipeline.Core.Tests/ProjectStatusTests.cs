// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core.Tests;

public class ProjectStatusTests
{
    [Test]
    public void Idea_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectStatus.Idea, Is.EqualTo(0));
    }

    [Test]
    public void Planned_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectStatus.Planned, Is.EqualTo(1));
    }

    [Test]
    public void InProgress_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectStatus.InProgress, Is.EqualTo(2));
    }

    [Test]
    public void OnHold_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectStatus.OnHold, Is.EqualTo(3));
    }

    [Test]
    public void Completed_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectStatus.Completed, Is.EqualTo(4));
    }

    [Test]
    public void Cancelled_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectStatus.Cancelled, Is.EqualTo(5));
    }

    [Test]
    public void AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var idea = ProjectStatus.Idea;
        var planned = ProjectStatus.Planned;
        var inProgress = ProjectStatus.InProgress;
        var onHold = ProjectStatus.OnHold;
        var completed = ProjectStatus.Completed;
        var cancelled = ProjectStatus.Cancelled;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(idea, Is.EqualTo(ProjectStatus.Idea));
            Assert.That(planned, Is.EqualTo(ProjectStatus.Planned));
            Assert.That(inProgress, Is.EqualTo(ProjectStatus.InProgress));
            Assert.That(onHold, Is.EqualTo(ProjectStatus.OnHold));
            Assert.That(completed, Is.EqualTo(ProjectStatus.Completed));
            Assert.That(cancelled, Is.EqualTo(ProjectStatus.Cancelled));
        });
    }
}
