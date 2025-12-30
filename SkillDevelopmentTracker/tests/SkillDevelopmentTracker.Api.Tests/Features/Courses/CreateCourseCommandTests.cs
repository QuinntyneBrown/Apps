using SkillDevelopmentTracker.Api.Features.Courses;
using SkillDevelopmentTracker.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace SkillDevelopmentTracker.Api.Tests.Features.Courses;

[TestFixture]
public class CreateCourseCommandTests
{
    private Mock<ISkillDevelopmentTrackerContext> _contextMock = null!;
    private Mock<ILogger<CreateCourseCommandHandler>> _loggerMock = null!;
    private CreateCourseCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ISkillDevelopmentTrackerContext>();
        _loggerMock = new Mock<ILogger<CreateCourseCommandHandler>>();
        _handler = new CreateCourseCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesCourse()
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Advanced React Patterns",
            Provider = "Frontend Masters",
            Instructor = "Kent C. Dodds",
            CourseUrl = "https://frontendmasters.com/courses/advanced-react-patterns/",
            StartDate = new DateTime(2024, 1, 10),
            EstimatedHours = 8m,
            Notes = "Great course!",
        };

        var courses = new List<Course>();
        var mockDbSet = TestHelpers.CreateMockDbSet(courses);
        _contextMock.Setup(c => c.Courses).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Provider, Is.EqualTo(command.Provider));
        Assert.That(result.Instructor, Is.EqualTo(command.Instructor));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.ProgressPercentage, Is.EqualTo(0));
        Assert.That(result.IsCompleted, Is.False);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
