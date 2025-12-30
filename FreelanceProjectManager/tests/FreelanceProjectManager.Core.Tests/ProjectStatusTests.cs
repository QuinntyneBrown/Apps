// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;

namespace FreelanceProjectManager.Core.Tests;

public class ProjectStatusTests
{
    [Test]
    public void ProjectStatus_Planning_HasCorrectValue()
    {
        // Assert
        Assert.That(ProjectStatus.Planning, Is.EqualTo((ProjectStatus)0));
    }

    [Test]
    public void ProjectStatus_InProgress_HasCorrectValue()
    {
        // Assert
        Assert.That(ProjectStatus.InProgress, Is.EqualTo((ProjectStatus)1));
    }

    [Test]
    public void ProjectStatus_OnHold_HasCorrectValue()
    {
        // Assert
        Assert.That(ProjectStatus.OnHold, Is.EqualTo((ProjectStatus)2));
    }

    [Test]
    public void ProjectStatus_UnderReview_HasCorrectValue()
    {
        // Assert
        Assert.That(ProjectStatus.UnderReview, Is.EqualTo((ProjectStatus)3));
    }

    [Test]
    public void ProjectStatus_Completed_HasCorrectValue()
    {
        // Assert
        Assert.That(ProjectStatus.Completed, Is.EqualTo((ProjectStatus)4));
    }

    [Test]
    public void ProjectStatus_Cancelled_HasCorrectValue()
    {
        // Assert
        Assert.That(ProjectStatus.Cancelled, Is.EqualTo((ProjectStatus)5));
    }

    [Test]
    public void ProjectStatus_AllValues_CanBeAssigned()
    {
        // Arrange
        var allStatuses = new[]
        {
            ProjectStatus.Planning,
            ProjectStatus.InProgress,
            ProjectStatus.OnHold,
            ProjectStatus.UnderReview,
            ProjectStatus.Completed,
            ProjectStatus.Cancelled
        };

        // Act & Assert
        foreach (var status in allStatuses)
        {
            var project = new Project { Status = status };
            Assert.That(project.Status, Is.EqualTo(status));
        }
    }

    [Test]
    public void ProjectStatus_HasExpectedNumberOfValues()
    {
        // Arrange
        var enumValues = Enum.GetValues(typeof(ProjectStatus));

        // Assert
        Assert.That(enumValues.Length, Is.EqualTo(6));
    }
}
