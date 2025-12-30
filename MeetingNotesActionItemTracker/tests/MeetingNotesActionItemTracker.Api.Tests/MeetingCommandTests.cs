// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Api.Tests;

[TestFixture]
public class MeetingCommandTests
{
    [Test]
    public async Task CreateMeetingCommand_CreatesNewMeeting()
    {
        // Arrange
        var meetings = new List<Meeting>();
        var mockSet = TestHelpers.CreateMockDbSet(meetings);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.Meetings).Returns(mockSet.Object);

        var handler = new CreateMeetingCommandHandler(mockContext.Object);
        var command = new CreateMeetingCommand(
            UserId: Guid.NewGuid(),
            Title: "Test Meeting",
            MeetingDateTime: DateTime.UtcNow,
            DurationMinutes: 60,
            Location: "Conference Room",
            Attendees: new List<string> { "John", "Jane" },
            Agenda: "Discuss project",
            Summary: null
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(meetings.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateMeetingCommand_UpdatesExistingMeeting()
    {
        // Arrange
        var meetingId = Guid.NewGuid();
        var meetings = new List<Meeting>
        {
            new Meeting
            {
                MeetingId = meetingId,
                UserId = Guid.NewGuid(),
                Title = "Old Title",
                MeetingDateTime = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(meetings);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.Meetings).Returns(mockSet.Object);

        var handler = new UpdateMeetingCommandHandler(mockContext.Object);
        var command = new UpdateMeetingCommand(
            MeetingId: meetingId,
            Title: "Updated Title",
            MeetingDateTime: DateTime.UtcNow,
            DurationMinutes: 90,
            Location: "New Location",
            Attendees: new List<string> { "Alice", "Bob" },
            Agenda: "Updated agenda",
            Summary: "Updated summary"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Updated Title"));
        Assert.That(result.Location, Is.EqualTo("New Location"));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteMeetingCommand_DeletesExistingMeeting()
    {
        // Arrange
        var meetingId = Guid.NewGuid();
        var meetings = new List<Meeting>
        {
            new Meeting
            {
                MeetingId = meetingId,
                UserId = Guid.NewGuid(),
                Title = "Test Meeting",
                MeetingDateTime = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(meetings);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.Meetings).Returns(mockSet.Object);

        var handler = new DeleteMeetingCommandHandler(mockContext.Object);
        var command = new DeleteMeetingCommand(meetingId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(meetings.Count, Is.EqualTo(0));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void DeleteMeetingCommand_ThrowsWhenMeetingNotFound()
    {
        // Arrange
        var meetings = new List<Meeting>();
        var mockSet = TestHelpers.CreateMockDbSet(meetings);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.Meetings).Returns(mockSet.Object);

        var handler = new DeleteMeetingCommandHandler(mockContext.Object);
        var command = new DeleteMeetingCommand(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }
}
