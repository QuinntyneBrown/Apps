// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Api.Tests;

/// <summary>
/// Tests for ProjectTask features.
/// </summary>
[TestFixture]
public class ProjectTaskTests
{
    [Test]
    public void ProjectTaskDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var task = new Core.ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            MilestoneId = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Test Description",
            DueDate = DateTime.UtcNow.AddDays(7),
            IsCompleted = false,
            CompletionDate = null,
            EstimatedHours = 5.5,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = task.ToDto();

        // Assert
        Assert.That(dto.ProjectTaskId, Is.EqualTo(task.ProjectTaskId));
        Assert.That(dto.ProjectId, Is.EqualTo(task.ProjectId));
        Assert.That(dto.MilestoneId, Is.EqualTo(task.MilestoneId));
        Assert.That(dto.Title, Is.EqualTo(task.Title));
        Assert.That(dto.Description, Is.EqualTo(task.Description));
        Assert.That(dto.DueDate, Is.EqualTo(task.DueDate));
        Assert.That(dto.IsCompleted, Is.EqualTo(task.IsCompleted));
        Assert.That(dto.CompletionDate, Is.EqualTo(task.CompletionDate));
        Assert.That(dto.EstimatedHours, Is.EqualTo(task.EstimatedHours));
        Assert.That(dto.CreatedAt, Is.EqualTo(task.CreatedAt));
    }

    [Test]
    public async Task CreateProjectTaskCommand_CreatesTask()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var handler = new CreateProjectTaskCommandHandler(context);
        var command = new CreateProjectTaskCommand
        {
            ProjectId = Guid.NewGuid(),
            MilestoneId = Guid.NewGuid(),
            Title = "New Task",
            Description = "New Description",
            DueDate = DateTime.UtcNow.AddDays(7),
            EstimatedHours = 4.0
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.ProjectId, Is.EqualTo(command.ProjectId));
        Assert.That(result.MilestoneId, Is.EqualTo(command.MilestoneId));
        Assert.That(result.EstimatedHours, Is.EqualTo(command.EstimatedHours));
        Assert.That(result.IsCompleted, Is.False);

        var savedTask = await context.Tasks.FindAsync(result.ProjectTaskId);
        Assert.That(savedTask, Is.Not.Null);
        Assert.That(savedTask!.Title, Is.EqualTo(command.Title));
    }

    [Test]
    public async Task UpdateProjectTaskCommand_UpdatesTask()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var task = new Core.ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Title = "Original Title",
            Description = "Original Description",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var handler = new UpdateProjectTaskCommandHandler(context);
        var command = new UpdateProjectTaskCommand
        {
            ProjectTaskId = task.ProjectTaskId,
            Title = "Updated Title",
            Description = "Updated Description",
            DueDate = DateTime.UtcNow.AddDays(10),
            IsCompleted = true,
            EstimatedHours = 6.0
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.IsCompleted, Is.True);
        Assert.That(result.CompletionDate, Is.Not.Null);
        Assert.That(result.EstimatedHours, Is.EqualTo(command.EstimatedHours));
    }

    [Test]
    public async Task DeleteProjectTaskCommand_DeletesTask()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var task = new Core.ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Title = "Test Task",
            CreatedAt = DateTime.UtcNow
        };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var handler = new DeleteProjectTaskCommandHandler(context);
        var command = new DeleteProjectTaskCommand { ProjectTaskId = task.ProjectTaskId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedTask = await context.Tasks.FindAsync(task.ProjectTaskId);
        Assert.That(deletedTask, Is.Null);
    }

    [Test]
    public async Task GetProjectTasksQuery_ReturnsAllTasks()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var projectId = Guid.NewGuid();
        var task1 = new Core.ProjectTask { ProjectTaskId = Guid.NewGuid(), ProjectId = projectId, Title = "Task 1", CreatedAt = DateTime.UtcNow };
        var task2 = new Core.ProjectTask { ProjectTaskId = Guid.NewGuid(), ProjectId = projectId, Title = "Task 2", CreatedAt = DateTime.UtcNow.AddMinutes(1) };
        context.Tasks.Add(task1);
        context.Tasks.Add(task2);
        await context.SaveChangesAsync();

        var handler = new GetProjectTasksQueryHandler(context);
        var query = new GetProjectTasksQuery { ProjectId = projectId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetProjectTaskByIdQuery_ReturnsTask()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var task = new Core.ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Title = "Test Task",
            CreatedAt = DateTime.UtcNow
        };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var handler = new GetProjectTaskByIdQueryHandler(context);
        var query = new GetProjectTaskByIdQuery { ProjectTaskId = task.ProjectTaskId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ProjectTaskId, Is.EqualTo(task.ProjectTaskId));
        Assert.That(result.Title, Is.EqualTo(task.Title));
    }
}
