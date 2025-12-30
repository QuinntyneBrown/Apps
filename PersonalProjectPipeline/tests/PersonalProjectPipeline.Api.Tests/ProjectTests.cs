// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Api.Tests;

/// <summary>
/// Tests for Project features.
/// </summary>
[TestFixture]
public class ProjectTests
{
    [Test]
    public void ProjectDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Description = "Test Description",
            Status = ProjectStatus.InProgress,
            Priority = ProjectPriority.High,
            StartDate = DateTime.UtcNow,
            TargetDate = DateTime.UtcNow.AddDays(30),
            CompletionDate = null,
            Tags = "test,project",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = project.ToDto();

        // Assert
        Assert.That(dto.ProjectId, Is.EqualTo(project.ProjectId));
        Assert.That(dto.UserId, Is.EqualTo(project.UserId));
        Assert.That(dto.Name, Is.EqualTo(project.Name));
        Assert.That(dto.Description, Is.EqualTo(project.Description));
        Assert.That(dto.Status, Is.EqualTo(project.Status));
        Assert.That(dto.Priority, Is.EqualTo(project.Priority));
        Assert.That(dto.StartDate, Is.EqualTo(project.StartDate));
        Assert.That(dto.TargetDate, Is.EqualTo(project.TargetDate));
        Assert.That(dto.CompletionDate, Is.EqualTo(project.CompletionDate));
        Assert.That(dto.Tags, Is.EqualTo(project.Tags));
        Assert.That(dto.CreatedAt, Is.EqualTo(project.CreatedAt));
    }

    [Test]
    public async Task CreateProjectCommand_CreatesProject()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var handler = new CreateProjectCommandHandler(context);
        var command = new CreateProjectCommand
        {
            UserId = Guid.NewGuid(),
            Name = "New Project",
            Description = "New Description",
            Status = ProjectStatus.Planned,
            Priority = ProjectPriority.Medium,
            StartDate = DateTime.UtcNow,
            TargetDate = DateTime.UtcNow.AddDays(30),
            Tags = "new,project"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.Status, Is.EqualTo(command.Status));
        Assert.That(result.Priority, Is.EqualTo(command.Priority));
        Assert.That(result.Tags, Is.EqualTo(command.Tags));

        var savedProject = await context.Projects.FindAsync(result.ProjectId);
        Assert.That(savedProject, Is.Not.Null);
        Assert.That(savedProject!.Name, Is.EqualTo(command.Name));
    }

    [Test]
    public async Task UpdateProjectCommand_UpdatesProject()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Original Name",
            Description = "Original Description",
            Status = ProjectStatus.Planned,
            Priority = ProjectPriority.Low,
            CreatedAt = DateTime.UtcNow
        };
        context.Projects.Add(project);
        await context.SaveChangesAsync();

        var handler = new UpdateProjectCommandHandler(context);
        var command = new UpdateProjectCommand
        {
            ProjectId = project.ProjectId,
            Name = "Updated Name",
            Description = "Updated Description",
            Status = ProjectStatus.InProgress,
            Priority = ProjectPriority.High,
            StartDate = DateTime.UtcNow,
            TargetDate = DateTime.UtcNow.AddDays(30),
            Tags = "updated"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.Status, Is.EqualTo(command.Status));
        Assert.That(result.Priority, Is.EqualTo(command.Priority));
        Assert.That(result.Tags, Is.EqualTo(command.Tags));
    }

    [Test]
    public async Task DeleteProjectCommand_DeletesProject()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            CreatedAt = DateTime.UtcNow
        };
        context.Projects.Add(project);
        await context.SaveChangesAsync();

        var handler = new DeleteProjectCommandHandler(context);
        var command = new DeleteProjectCommand { ProjectId = project.ProjectId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedProject = await context.Projects.FindAsync(project.ProjectId);
        Assert.That(deletedProject, Is.Null);
    }

    [Test]
    public async Task GetProjectsQuery_ReturnsAllProjects()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var userId = Guid.NewGuid();
        var project1 = new Project { ProjectId = Guid.NewGuid(), UserId = userId, Name = "Project 1", CreatedAt = DateTime.UtcNow };
        var project2 = new Project { ProjectId = Guid.NewGuid(), UserId = userId, Name = "Project 2", CreatedAt = DateTime.UtcNow.AddMinutes(1) };
        context.Projects.Add(project1);
        context.Projects.Add(project2);
        await context.SaveChangesAsync();

        var handler = new GetProjectsQueryHandler(context);
        var query = new GetProjectsQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetProjectByIdQuery_ReturnsProject()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            CreatedAt = DateTime.UtcNow
        };
        context.Projects.Add(project);
        await context.SaveChangesAsync();

        var handler = new GetProjectByIdQueryHandler(context);
        var query = new GetProjectByIdQuery { ProjectId = project.ProjectId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ProjectId, Is.EqualTo(project.ProjectId));
        Assert.That(result.Name, Is.EqualTo(project.Name));
    }
}
