// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core.Tests;

public class ProjectPhaseTests
{
    [Test]
    public void ProjectPhase_Planning_HasCorrectValue()
    {
        // Arrange & Act
        var phase = ProjectPhase.Planning;

        // Assert
        Assert.That((int)phase, Is.EqualTo(0));
    }

    [Test]
    public void ProjectPhase_Disassembly_HasCorrectValue()
    {
        // Arrange & Act
        var phase = ProjectPhase.Disassembly;

        // Assert
        Assert.That((int)phase, Is.EqualTo(1));
    }

    [Test]
    public void ProjectPhase_Cleaning_HasCorrectValue()
    {
        // Arrange & Act
        var phase = ProjectPhase.Cleaning;

        // Assert
        Assert.That((int)phase, Is.EqualTo(2));
    }

    [Test]
    public void ProjectPhase_Repair_HasCorrectValue()
    {
        // Arrange & Act
        var phase = ProjectPhase.Repair;

        // Assert
        Assert.That((int)phase, Is.EqualTo(3));
    }

    [Test]
    public void ProjectPhase_Painting_HasCorrectValue()
    {
        // Arrange & Act
        var phase = ProjectPhase.Painting;

        // Assert
        Assert.That((int)phase, Is.EqualTo(4));
    }

    [Test]
    public void ProjectPhase_Reassembly_HasCorrectValue()
    {
        // Arrange & Act
        var phase = ProjectPhase.Reassembly;

        // Assert
        Assert.That((int)phase, Is.EqualTo(5));
    }

    [Test]
    public void ProjectPhase_Testing_HasCorrectValue()
    {
        // Arrange & Act
        var phase = ProjectPhase.Testing;

        // Assert
        Assert.That((int)phase, Is.EqualTo(6));
    }

    [Test]
    public void ProjectPhase_Completed_HasCorrectValue()
    {
        // Arrange & Act
        var phase = ProjectPhase.Completed;

        // Assert
        Assert.That((int)phase, Is.EqualTo(7));
    }

    [Test]
    public void ProjectPhase_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var planning = ProjectPhase.Planning;
        var disassembly = ProjectPhase.Disassembly;
        var cleaning = ProjectPhase.Cleaning;
        var repair = ProjectPhase.Repair;
        var painting = ProjectPhase.Painting;
        var reassembly = ProjectPhase.Reassembly;
        var testing = ProjectPhase.Testing;
        var completed = ProjectPhase.Completed;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(planning, Is.EqualTo(ProjectPhase.Planning));
            Assert.That(disassembly, Is.EqualTo(ProjectPhase.Disassembly));
            Assert.That(cleaning, Is.EqualTo(ProjectPhase.Cleaning));
            Assert.That(repair, Is.EqualTo(ProjectPhase.Repair));
            Assert.That(painting, Is.EqualTo(ProjectPhase.Painting));
            Assert.That(reassembly, Is.EqualTo(ProjectPhase.Reassembly));
            Assert.That(testing, Is.EqualTo(ProjectPhase.Testing));
            Assert.That(completed, Is.EqualTo(ProjectPhase.Completed));
        });
    }
}
