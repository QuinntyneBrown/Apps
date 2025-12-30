using LifeAdminDashboard.Api.Features.Deadlines;
using Microsoft.Extensions.Logging;
using Moq;

namespace LifeAdminDashboard.Api.Tests.Features.Deadlines;

[TestFixture]
public class CreateDeadlineCommandTests
{
    private Mock<ILifeAdminDashboardContext> _contextMock = null!;
    private Mock<ILogger<CreateDeadlineCommandHandler>> _loggerMock = null!;
    private CreateDeadlineCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ILifeAdminDashboardContext>();
        _loggerMock = new Mock<ILogger<CreateDeadlineCommandHandler>>();
        _handler = new CreateDeadlineCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesDeadline()
    {
        // Arrange
        var command = new CreateDeadlineCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Tax Filing Deadline",
            Description = "Submit tax returns",
            DeadlineDateTime = DateTime.UtcNow.AddDays(30),
            Category = "Financial",
            RemindersEnabled = true,
            ReminderDaysAdvance = 7,
            Notes = "Gather all documents",
        };

        var deadlines = new List<Deadline>();
        var mockDbSet = TestHelpers.CreateMockDbSet(deadlines);
        _contextMock.Setup(c => c.Deadlines).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.DeadlineDateTime, Is.EqualTo(command.DeadlineDateTime));
        Assert.That(result.Category, Is.EqualTo(command.Category));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.IsCompleted, Is.False);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithReminders_IncludesReminderSettings()
    {
        // Arrange
        var command = new CreateDeadlineCommand
        {
            UserId = Guid.NewGuid(),
            Title = "License Renewal",
            DeadlineDateTime = DateTime.UtcNow.AddDays(60),
            RemindersEnabled = true,
            ReminderDaysAdvance = 14,
        };

        var deadlines = new List<Deadline>();
        var mockDbSet = TestHelpers.CreateMockDbSet(deadlines);
        _contextMock.Setup(c => c.Deadlines).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.RemindersEnabled, Is.True);
        Assert.That(result.ReminderDaysAdvance, Is.EqualTo(14));
    }
}
