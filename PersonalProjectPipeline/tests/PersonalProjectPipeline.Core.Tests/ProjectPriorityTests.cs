// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core.Tests;

public class ProjectPriorityTests
{
    [Test]
    public void Low_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectPriority.Low, Is.EqualTo(0));
    }

    [Test]
    public void Medium_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectPriority.Medium, Is.EqualTo(1));
    }

    [Test]
    public void High_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectPriority.High, Is.EqualTo(2));
    }

    [Test]
    public void Critical_HasCorrectValue()
    {
        // Assert
        Assert.That((int)ProjectPriority.Critical, Is.EqualTo(3));
    }

    [Test]
    public void AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var low = ProjectPriority.Low;
        var medium = ProjectPriority.Medium;
        var high = ProjectPriority.High;
        var critical = ProjectPriority.Critical;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(low, Is.EqualTo(ProjectPriority.Low));
            Assert.That(medium, Is.EqualTo(ProjectPriority.Medium));
            Assert.That(high, Is.EqualTo(ProjectPriority.High));
            Assert.That(critical, Is.EqualTo(ProjectPriority.Critical));
        });
    }
}
