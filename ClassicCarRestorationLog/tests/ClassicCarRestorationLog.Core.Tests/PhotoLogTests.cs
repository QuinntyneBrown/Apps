// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core.Tests;

public class PhotoLogTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var photoLog = new PhotoLog();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(photoLog.Description, Is.Null);
            Assert.That(photoLog.PhotoUrl, Is.Null);
            Assert.That(photoLog.Phase, Is.Null);
            Assert.That(photoLog.Project, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var photoLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var photoDate = DateTime.UtcNow.AddDays(-5);

        // Act
        var photoLog = new PhotoLog
        {
            PhotoLogId = photoLogId,
            UserId = userId,
            ProjectId = projectId,
            PhotoDate = photoDate,
            Description = "Before restoration",
            PhotoUrl = "https://example.com/photo.jpg",
            Phase = ProjectPhase.Planning
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(photoLog.PhotoLogId, Is.EqualTo(photoLogId));
            Assert.That(photoLog.UserId, Is.EqualTo(userId));
            Assert.That(photoLog.ProjectId, Is.EqualTo(projectId));
            Assert.That(photoLog.PhotoDate, Is.EqualTo(photoDate));
            Assert.That(photoLog.Description, Is.EqualTo("Before restoration"));
            Assert.That(photoLog.PhotoUrl, Is.EqualTo("https://example.com/photo.jpg"));
            Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Planning));
        });
    }

    [Test]
    public void Phase_CanBeSetToAllValues()
    {
        // Arrange
        var photoLog = new PhotoLog();

        // Act & Assert
        photoLog.Phase = ProjectPhase.Planning;
        Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Planning));

        photoLog.Phase = ProjectPhase.Disassembly;
        Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Disassembly));

        photoLog.Phase = ProjectPhase.Cleaning;
        Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Cleaning));

        photoLog.Phase = ProjectPhase.Repair;
        Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Repair));

        photoLog.Phase = ProjectPhase.Painting;
        Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Painting));

        photoLog.Phase = ProjectPhase.Reassembly;
        Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Reassembly));

        photoLog.Phase = ProjectPhase.Testing;
        Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Testing));

        photoLog.Phase = ProjectPhase.Completed;
        Assert.That(photoLog.Phase, Is.EqualTo(ProjectPhase.Completed));
    }

    [Test]
    public void Phase_CanBeNull()
    {
        // Arrange & Act
        var photoLog = new PhotoLog
        {
            Phase = null
        };

        // Assert
        Assert.That(photoLog.Phase, Is.Null);
    }

    [Test]
    public void Project_CanBeAssigned()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            CarMake = "Porsche",
            CarModel = "911"
        };
        var photoLog = new PhotoLog();

        // Act
        photoLog.Project = project;

        // Assert
        Assert.That(photoLog.Project, Is.EqualTo(project));
    }
}
