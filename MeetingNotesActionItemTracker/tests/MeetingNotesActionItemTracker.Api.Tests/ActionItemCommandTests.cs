// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Api.Tests;

[TestFixture]
public class ActionItemCommandTests
{
    [Test]
    public async Task CreateActionItemCommand_CreatesNewActionItem()
    {
        // Arrange
        var actionItems = new List<Core.ActionItem>();
        var mockSet = TestHelpers.CreateMockDbSet(actionItems);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.ActionItems).Returns(mockSet.Object);

        var handler = new CreateActionItemCommandHandler(mockContext.Object);
        var command = new CreateActionItemCommand(
            UserId: Guid.NewGuid(),
            MeetingId: Guid.NewGuid(),
            Description: "Complete task",
            ResponsiblePerson: "John Doe",
            DueDate: DateTime.UtcNow.AddDays(7),
            Priority: Priority.High,
            Status: ActionItemStatus.NotStarted,
            Notes: "Task notes"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.Priority, Is.EqualTo(Priority.High));
        Assert.That(actionItems.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateActionItemCommand_UpdatesExistingActionItem()
    {
        // Arrange
        var actionItemId = Guid.NewGuid();
        var actionItems = new List<Core.ActionItem>
        {
            new Core.ActionItem
            {
                ActionItemId = actionItemId,
                UserId = Guid.NewGuid(),
                MeetingId = Guid.NewGuid(),
                Description = "Old description",
                Priority = Priority.Low,
                Status = ActionItemStatus.NotStarted,
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(actionItems);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.ActionItems).Returns(mockSet.Object);

        var handler = new UpdateActionItemCommandHandler(mockContext.Object);
        var command = new UpdateActionItemCommand(
            ActionItemId: actionItemId,
            Description: "Updated description",
            ResponsiblePerson: "Jane Doe",
            DueDate: DateTime.UtcNow.AddDays(14),
            Priority: Priority.Critical,
            Status: ActionItemStatus.InProgress,
            Notes: "Updated notes"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Description, Is.EqualTo("Updated description"));
        Assert.That(result.Priority, Is.EqualTo(Priority.Critical));
        Assert.That(result.Status, Is.EqualTo(ActionItemStatus.InProgress));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateActionItemCommand_SetsCompletedDateWhenStatusCompleted()
    {
        // Arrange
        var actionItemId = Guid.NewGuid();
        var actionItems = new List<Core.ActionItem>
        {
            new Core.ActionItem
            {
                ActionItemId = actionItemId,
                UserId = Guid.NewGuid(),
                MeetingId = Guid.NewGuid(),
                Description = "Task",
                Priority = Priority.Medium,
                Status = ActionItemStatus.InProgress,
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(actionItems);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.ActionItems).Returns(mockSet.Object);

        var handler = new UpdateActionItemCommandHandler(mockContext.Object);
        var command = new UpdateActionItemCommand(
            ActionItemId: actionItemId,
            Description: "Task",
            ResponsiblePerson: "John",
            DueDate: DateTime.UtcNow,
            Priority: Priority.Medium,
            Status: ActionItemStatus.Completed,
            Notes: null
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Status, Is.EqualTo(ActionItemStatus.Completed));
        Assert.That(result.CompletedDate, Is.Not.Null);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteActionItemCommand_DeletesExistingActionItem()
    {
        // Arrange
        var actionItemId = Guid.NewGuid();
        var actionItems = new List<Core.ActionItem>
        {
            new Core.ActionItem
            {
                ActionItemId = actionItemId,
                UserId = Guid.NewGuid(),
                MeetingId = Guid.NewGuid(),
                Description = "Test action item",
                Priority = Priority.Low,
                Status = ActionItemStatus.NotStarted,
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(actionItems);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.ActionItems).Returns(mockSet.Object);

        var handler = new DeleteActionItemCommandHandler(mockContext.Object);
        var command = new DeleteActionItemCommand(actionItemId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(actionItems.Count, Is.EqualTo(0));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
