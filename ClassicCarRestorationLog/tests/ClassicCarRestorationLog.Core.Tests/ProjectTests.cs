// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core.Tests;

public class ProjectTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var project = new Project();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.CarMake, Is.EqualTo(string.Empty));
            Assert.That(project.CarModel, Is.EqualTo(string.Empty));
            Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Planning));
            Assert.That(project.CompletionDate, Is.Null);
            Assert.That(project.Parts, Is.Not.Null);
            Assert.That(project.WorkLogs, Is.Not.Null);
            Assert.That(project.PhotoLogs, Is.Not.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startDate = DateTime.UtcNow;
        var completionDate = DateTime.UtcNow.AddMonths(6);

        // Act
        var project = new Project
        {
            ProjectId = projectId,
            UserId = userId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Year = 1967,
            Phase = ProjectPhase.Repair,
            StartDate = startDate,
            CompletionDate = completionDate,
            EstimatedBudget = 50000m,
            ActualCost = 35000m,
            Notes = "Classic restoration project"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(projectId));
            Assert.That(project.UserId, Is.EqualTo(userId));
            Assert.That(project.CarMake, Is.EqualTo("Ford"));
            Assert.That(project.CarModel, Is.EqualTo("Mustang"));
            Assert.That(project.Year, Is.EqualTo(1967));
            Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Repair));
            Assert.That(project.StartDate, Is.EqualTo(startDate));
            Assert.That(project.CompletionDate, Is.EqualTo(completionDate));
            Assert.That(project.EstimatedBudget, Is.EqualTo(50000m));
            Assert.That(project.ActualCost, Is.EqualTo(35000m));
            Assert.That(project.Notes, Is.EqualTo("Classic restoration project"));
        });
    }

    [Test]
    public void Phase_CanBeSetToDifferentValues()
    {
        // Arrange & Act
        var project = new Project();

        // Assert initial
        Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Planning));

        // Act & Assert for each phase
        project.Phase = ProjectPhase.Disassembly;
        Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Disassembly));

        project.Phase = ProjectPhase.Cleaning;
        Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Cleaning));

        project.Phase = ProjectPhase.Repair;
        Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Repair));

        project.Phase = ProjectPhase.Painting;
        Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Painting));

        project.Phase = ProjectPhase.Reassembly;
        Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Reassembly));

        project.Phase = ProjectPhase.Testing;
        Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Testing));

        project.Phase = ProjectPhase.Completed;
        Assert.That(project.Phase, Is.EqualTo(ProjectPhase.Completed));
    }

    [Test]
    public void Collections_CanAddItems()
    {
        // Arrange
        var project = new Project();
        var part = new Part { PartId = Guid.NewGuid() };
        var workLog = new WorkLog { WorkLogId = Guid.NewGuid() };
        var photoLog = new PhotoLog { PhotoLogId = Guid.NewGuid() };

        // Act
        project.Parts.Add(part);
        project.WorkLogs.Add(workLog);
        project.PhotoLogs.Add(photoLog);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Parts, Has.Count.EqualTo(1));
            Assert.That(project.WorkLogs, Has.Count.EqualTo(1));
            Assert.That(project.PhotoLogs, Has.Count.EqualTo(1));
            Assert.That(project.Parts.First(), Is.EqualTo(part));
            Assert.That(project.WorkLogs.First(), Is.EqualTo(workLog));
            Assert.That(project.PhotoLogs.First(), Is.EqualTo(photoLog));
        });
    }

    [Test]
    public void Budget_CanBeNull()
    {
        // Arrange & Act
        var project = new Project
        {
            EstimatedBudget = null,
            ActualCost = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.EstimatedBudget, Is.Null);
            Assert.That(project.ActualCost, Is.Null);
        });
    }

    [Test]
    public void Year_CanBeNull()
    {
        // Arrange & Act
        var project = new Project
        {
            Year = null
        };

        // Assert
        Assert.That(project.Year, Is.Null);
    }
}
