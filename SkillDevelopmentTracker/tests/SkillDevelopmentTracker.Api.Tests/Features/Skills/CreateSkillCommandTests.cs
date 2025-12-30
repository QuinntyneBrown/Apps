using SkillDevelopmentTracker.Api.Features.Skills;
using SkillDevelopmentTracker.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace SkillDevelopmentTracker.Api.Tests.Features.Skills;

[TestFixture]
public class CreateSkillCommandTests
{
    private Mock<ISkillDevelopmentTrackerContext> _contextMock = null!;
    private Mock<ILogger<CreateSkillCommandHandler>> _loggerMock = null!;
    private CreateSkillCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ISkillDevelopmentTrackerContext>();
        _loggerMock = new Mock<ILogger<CreateSkillCommandHandler>>();
        _handler = new CreateSkillCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesSkill()
    {
        // Arrange
        var command = new CreateSkillCommand
        {
            UserId = Guid.NewGuid(),
            Name = "React",
            Category = "Programming",
            ProficiencyLevel = ProficiencyLevel.Intermediate,
            TargetLevel = ProficiencyLevel.Advanced,
            StartDate = new DateTime(2023, 1, 1),
            TargetDate = new DateTime(2024, 12, 31),
            HoursSpent = 120m,
            Notes = "Building modern web applications",
        };

        var skills = new List<Skill>();
        var mockDbSet = TestHelpers.CreateMockDbSet(skills);
        _contextMock.Setup(c => c.Skills).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Category, Is.EqualTo(command.Category));
        Assert.That(result.ProficiencyLevel, Is.EqualTo(command.ProficiencyLevel));
        Assert.That(result.TargetLevel, Is.EqualTo(command.TargetLevel));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
