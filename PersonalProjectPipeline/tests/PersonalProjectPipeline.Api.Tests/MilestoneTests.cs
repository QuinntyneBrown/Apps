// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Api.Tests;

/// <summary>
/// Tests for Milestone features.
/// </summary>
[TestFixture]
public class MilestoneTests
{
    [Test]
    public void MilestoneDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var milestone = new Core.Milestone
        {
            MilestoneId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Test Milestone",
            Description = "Test Description",
            TargetDate = DateTime.UtcNow.AddDays(30),
            IsAchieved = false,
            AchievementDate = null,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = milestone.ToDto();

        // Assert
        Assert.That(dto.MilestoneId, Is.EqualTo(milestone.MilestoneId));
        Assert.That(dto.ProjectId, Is.EqualTo(milestone.ProjectId));
        Assert.That(dto.Name, Is.EqualTo(milestone.Name));
        Assert.That(dto.Description, Is.EqualTo(milestone.Description));
        Assert.That(dto.TargetDate, Is.EqualTo(milestone.TargetDate));
        Assert.That(dto.IsAchieved, Is.EqualTo(milestone.IsAchieved));
        Assert.That(dto.AchievementDate, Is.EqualTo(milestone.AchievementDate));
        Assert.That(dto.CreatedAt, Is.EqualTo(milestone.CreatedAt));
    }

    [Test]
    public async Task CreateMilestoneCommand_CreatesMilestone()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var handler = new CreateMilestoneCommandHandler(context);
        var command = new CreateMilestoneCommand
        {
            ProjectId = Guid.NewGuid(),
            Name = "New Milestone",
            Description = "New Description",
            TargetDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.ProjectId, Is.EqualTo(command.ProjectId));
        Assert.That(result.TargetDate, Is.EqualTo(command.TargetDate));
        Assert.That(result.IsAchieved, Is.False);

        var savedMilestone = await context.Milestones.FindAsync(result.MilestoneId);
        Assert.That(savedMilestone, Is.Not.Null);
        Assert.That(savedMilestone!.Name, Is.EqualTo(command.Name));
    }

    [Test]
    public async Task UpdateMilestoneCommand_UpdatesMilestone()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var milestone = new Core.Milestone
        {
            MilestoneId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Original Name",
            Description = "Original Description",
            IsAchieved = false,
            CreatedAt = DateTime.UtcNow
        };
        context.Milestones.Add(milestone);
        await context.SaveChangesAsync();

        var handler = new UpdateMilestoneCommandHandler(context);
        var command = new UpdateMilestoneCommand
        {
            MilestoneId = milestone.MilestoneId,
            Name = "Updated Name",
            Description = "Updated Description",
            TargetDate = DateTime.UtcNow.AddDays(60),
            IsAchieved = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.IsAchieved, Is.True);
        Assert.That(result.AchievementDate, Is.Not.Null);
    }

    [Test]
    public async Task DeleteMilestoneCommand_DeletesMilestone()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var milestone = new Core.Milestone
        {
            MilestoneId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Test Milestone",
            CreatedAt = DateTime.UtcNow
        };
        context.Milestones.Add(milestone);
        await context.SaveChangesAsync();

        var handler = new DeleteMilestoneCommandHandler(context);
        var command = new DeleteMilestoneCommand { MilestoneId = milestone.MilestoneId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedMilestone = await context.Milestones.FindAsync(milestone.MilestoneId);
        Assert.That(deletedMilestone, Is.Null);
    }

    [Test]
    public async Task GetMilestonesQuery_ReturnsAllMilestones()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var projectId = Guid.NewGuid();
        var milestone1 = new Core.Milestone { MilestoneId = Guid.NewGuid(), ProjectId = projectId, Name = "Milestone 1", CreatedAt = DateTime.UtcNow };
        var milestone2 = new Core.Milestone { MilestoneId = Guid.NewGuid(), ProjectId = projectId, Name = "Milestone 2", CreatedAt = DateTime.UtcNow.AddMinutes(1) };
        context.Milestones.Add(milestone1);
        context.Milestones.Add(milestone2);
        await context.SaveChangesAsync();

        var handler = new GetMilestonesQueryHandler(context);
        var query = new GetMilestonesQuery { ProjectId = projectId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetMilestoneByIdQuery_ReturnsMilestone()
    {
        // Arrange
        var context = TestHelpers.CreateInMemoryContext();
        var milestone = new Core.Milestone
        {
            MilestoneId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Test Milestone",
            CreatedAt = DateTime.UtcNow
        };
        context.Milestones.Add(milestone);
        await context.SaveChangesAsync();

        var handler = new GetMilestoneByIdQueryHandler(context);
        var query = new GetMilestoneByIdQuery { MilestoneId = milestone.MilestoneId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.MilestoneId, Is.EqualTo(milestone.MilestoneId));
        Assert.That(result.Name, Is.EqualTo(milestone.Name));
    }
}
