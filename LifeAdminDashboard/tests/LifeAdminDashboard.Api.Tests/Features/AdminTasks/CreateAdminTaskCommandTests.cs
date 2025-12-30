using LifeAdminDashboard.Api.Features.AdminTasks;
using Microsoft.Extensions.Logging;
using Moq;

namespace LifeAdminDashboard.Api.Tests.Features.AdminTasks;

[TestFixture]
public class CreateAdminTaskCommandTests
{
    private Mock<ILifeAdminDashboardContext> _contextMock = null!;
    private Mock<ILogger<CreateAdminTaskCommandHandler>> _loggerMock = null!;
    private CreateAdminTaskCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ILifeAdminDashboardContext>();
        _loggerMock = new Mock<ILogger<CreateAdminTaskCommandHandler>>();
        _handler = new CreateAdminTaskCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesAdminTask()
    {
        // Arrange
        var command = new CreateAdminTaskCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Pay taxes",
            Description = "File and pay annual taxes",
            Category = TaskCategory.Financial,
            Priority = TaskPriority.High,
            DueDate = DateTime.UtcNow.AddDays(30),
            IsRecurring = true,
            RecurrencePattern = "Yearly",
            Notes = "Don't forget deductions",
        };

        var tasks = new List<AdminTask>();
        var mockDbSet = TestHelpers.CreateMockDbSet(tasks);
        _contextMock.Setup(c => c.Tasks).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.Category, Is.EqualTo(command.Category));
        Assert.That(result.Priority, Is.EqualTo(command.Priority));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.IsCompleted, Is.False);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithDueDate_IncludesDueDateInTask()
    {
        // Arrange
        var dueDate = DateTime.UtcNow.AddDays(7);
        var command = new CreateAdminTaskCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Renew license",
            Category = TaskCategory.Legal,
            Priority = TaskPriority.Medium,
            DueDate = dueDate,
        };

        var tasks = new List<AdminTask>();
        var mockDbSet = TestHelpers.CreateMockDbSet(tasks);
        _contextMock.Setup(c => c.Tasks).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.DueDate, Is.EqualTo(dueDate));
    }
}
