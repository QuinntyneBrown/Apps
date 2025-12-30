using FinancialGoalTracker.Api.Features.Goals;
using FinancialGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FinancialGoalTracker.Api.Tests.Features.Goals;

[TestFixture]
public class CreateGoalCommandHandlerTests
{
    private Mock<IFinancialGoalTrackerContext> _contextMock = null!;
    private Mock<DbSet<Goal>> _goalsDbSetMock = null!;
    private Mock<ILogger<CreateGoalCommandHandler>> _loggerMock = null!;
    private CreateGoalCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _contextMock = new Mock<IFinancialGoalTrackerContext>();
        _goalsDbSetMock = new Mock<DbSet<Goal>>();
        _loggerMock = new Mock<ILogger<CreateGoalCommandHandler>>();

        _contextMock.Setup(c => c.Goals).Returns(_goalsDbSetMock.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _handler = new CreateGoalCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_WithValidCommand_CreatesGoal()
    {
        // Arrange
        var command = new CreateGoalCommand
        {
            Name = "Emergency Fund",
            Description = "Save for emergencies",
            GoalType = GoalType.Emergency,
            TargetAmount = 10000,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            Notes = "Top priority",
        };

        Goal? capturedGoal = null;
        _goalsDbSetMock.Setup(d => d.Add(It.IsAny<Goal>()))
            .Callback<Goal>(g => capturedGoal = g);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.GoalType, Is.EqualTo(command.GoalType));
        Assert.That(result.TargetAmount, Is.EqualTo(command.TargetAmount));
        Assert.That(result.CurrentAmount, Is.EqualTo(0));
        Assert.That(result.Status, Is.EqualTo(GoalStatus.NotStarted));
        Assert.That(result.Notes, Is.EqualTo(command.Notes));

        Assert.That(capturedGoal, Is.Not.Null);
        Assert.That(capturedGoal!.GoalId, Is.Not.EqualTo(Guid.Empty));

        _goalsDbSetMock.Verify(d => d.Add(It.IsAny<Goal>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithValidCommand_ReturnsGoalWithProgress()
    {
        // Arrange
        var command = new CreateGoalCommand
        {
            Name = "Retirement",
            Description = "Save for retirement",
            GoalType = GoalType.Retirement,
            TargetAmount = 1000000,
            TargetDate = DateTime.UtcNow.AddYears(30),
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Progress, Is.EqualTo(0));
    }
}
