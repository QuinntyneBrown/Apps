using FinancialGoalTracker.Api.Features.Contributions;
using FinancialGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FinancialGoalTracker.Api.Tests.Features.Contributions;

[TestFixture]
public class CreateContributionCommandHandlerTests
{
    private Mock<IFinancialGoalTrackerContext> _contextMock = null!;
    private Mock<DbSet<Contribution>> _contributionsDbSetMock = null!;
    private Mock<DbSet<Goal>> _goalsDbSetMock = null!;
    private Mock<ILogger<CreateContributionCommandHandler>> _loggerMock = null!;
    private CreateContributionCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _contextMock = new Mock<IFinancialGoalTrackerContext>();
        _contributionsDbSetMock = new Mock<DbSet<Contribution>>();
        _goalsDbSetMock = new Mock<DbSet<Goal>>();
        _loggerMock = new Mock<ILogger<CreateContributionCommandHandler>>();

        _contextMock.Setup(c => c.Contributions).Returns(_contributionsDbSetMock.Object);
        _contextMock.Setup(c => c.Goals).Returns(_goalsDbSetMock.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _handler = new CreateContributionCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_WithValidCommand_CreatesContribution()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var command = new CreateContributionCommand
        {
            GoalId = goalId,
            Amount = 500,
            ContributionDate = DateTime.UtcNow,
            Notes = "Monthly contribution",
        };

        var goal = new Goal
        {
            GoalId = goalId,
            Name = "Emergency Fund",
            Description = "Save for emergencies",
            GoalType = GoalType.Emergency,
            TargetAmount = 10000,
            CurrentAmount = 5000,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            Status = GoalStatus.InProgress,
        };

        var goals = new List<Goal> { goal }.AsQueryable();
        var mockGoalsSet = new Mock<DbSet<Goal>>();
        mockGoalsSet.As<IQueryable<Goal>>().Setup(m => m.Provider).Returns(goals.Provider);
        mockGoalsSet.As<IQueryable<Goal>>().Setup(m => m.Expression).Returns(goals.Expression);
        mockGoalsSet.As<IQueryable<Goal>>().Setup(m => m.ElementType).Returns(goals.ElementType);
        mockGoalsSet.As<IQueryable<Goal>>().Setup(m => m.GetEnumerator()).Returns(goals.GetEnumerator());

        _contextMock.Setup(c => c.Goals).Returns(mockGoalsSet.Object);

        Contribution? capturedContribution = null;
        _contributionsDbSetMock.Setup(d => d.Add(It.IsAny<Contribution>()))
            .Callback<Contribution>(c => capturedContribution = c);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GoalId, Is.EqualTo(command.GoalId));
        Assert.That(result.Amount, Is.EqualTo(command.Amount));
        Assert.That(result.Notes, Is.EqualTo(command.Notes));

        Assert.That(capturedContribution, Is.Not.Null);
        Assert.That(capturedContribution!.ContributionId, Is.Not.EqualTo(Guid.Empty));

        // Verify goal progress was updated
        Assert.That(goal.CurrentAmount, Is.EqualTo(5500));

        _contributionsDbSetMock.Verify(d => d.Add(It.IsAny<Contribution>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
