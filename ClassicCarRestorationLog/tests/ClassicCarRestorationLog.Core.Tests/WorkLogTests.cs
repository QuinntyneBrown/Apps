// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core.Tests;

public class WorkLogTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var workLog = new WorkLog();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(workLog.Description, Is.EqualTo(string.Empty));
            Assert.That(workLog.HoursWorked, Is.EqualTo(0));
            Assert.That(workLog.WorkPerformed, Is.Null);
            Assert.That(workLog.Project, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var workLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var workDate = DateTime.UtcNow.AddDays(-1);

        // Act
        var workLog = new WorkLog
        {
            WorkLogId = workLogId,
            UserId = userId,
            ProjectId = projectId,
            WorkDate = workDate,
            HoursWorked = 8,
            Description = "Engine rebuild",
            WorkPerformed = "Removed and disassembled engine"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(workLog.WorkLogId, Is.EqualTo(workLogId));
            Assert.That(workLog.UserId, Is.EqualTo(userId));
            Assert.That(workLog.ProjectId, Is.EqualTo(projectId));
            Assert.That(workLog.WorkDate, Is.EqualTo(workDate));
            Assert.That(workLog.HoursWorked, Is.EqualTo(8));
            Assert.That(workLog.Description, Is.EqualTo("Engine rebuild"));
            Assert.That(workLog.WorkPerformed, Is.EqualTo("Removed and disassembled engine"));
        });
    }

    [Test]
    public void HoursWorked_CanBeZero()
    {
        // Arrange & Act
        var workLog = new WorkLog
        {
            HoursWorked = 0
        };

        // Assert
        Assert.That(workLog.HoursWorked, Is.EqualTo(0));
    }

    [Test]
    public void HoursWorked_CanBeNegative()
    {
        // Arrange & Act
        var workLog = new WorkLog
        {
            HoursWorked = -1
        };

        // Assert
        Assert.That(workLog.HoursWorked, Is.EqualTo(-1));
    }

    [Test]
    public void Project_CanBeAssigned()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            CarMake = "Ford",
            CarModel = "Mustang"
        };
        var workLog = new WorkLog();

        // Act
        workLog.Project = project;

        // Assert
        Assert.That(workLog.Project, Is.EqualTo(project));
    }
}
