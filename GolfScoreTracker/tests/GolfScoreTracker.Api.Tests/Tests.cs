using FluentAssertions;
using GolfScoreTracker.Api.Features.Courses;
using GolfScoreTracker.Core;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GolfScoreTracker.Api.Tests;

public class CourseCommandTests
{
    private Mock<IGolfScoreTrackerContext> _mockContext;
    private Mock<DbSet<Course>> _mockCourseSet;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IGolfScoreTrackerContext>();
        _mockCourseSet = new Mock<DbSet<Course>>();
    }

    [Test]
    public async Task CreateCourseCommand_ShouldCreateCourse()
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            Name = "Pebble Beach",
            Location = "California",
            NumberOfHoles = 18,
            TotalPar = 72,
            CourseRating = 75.5m,
            SlopeRating = 145
        };

        _mockContext.Setup(c => c.Courses).Returns(_mockCourseSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateCourseCommandHandler(_mockContext.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Pebble Beach");
        result.Location.Should().Be("California");
        result.NumberOfHoles.Should().Be(18);
        result.TotalPar.Should().Be(72);
        result.CourseRating.Should().Be(75.5m);
        result.SlopeRating.Should().Be(145);

        _mockCourseSet.Verify(m => m.Add(It.IsAny<Course>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreateCourseCommandValidator_ShouldFailWhenNameIsEmpty()
    {
        // Arrange
        var validator = new CreateCourseCommandValidator();
        var command = new CreateCourseCommand
        {
            Name = "",
            NumberOfHoles = 18,
            TotalPar = 72
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Test]
    public void CreateCourseCommandValidator_ShouldFailWhenNumberOfHolesIsInvalid()
    {
        // Arrange
        var validator = new CreateCourseCommandValidator();
        var command = new CreateCourseCommand
        {
            Name = "Test Course",
            NumberOfHoles = 20,
            TotalPar = 72
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "NumberOfHoles");
    }

    [Test]
    public void CreateCourseCommandValidator_ShouldPassWithValidData()
    {
        // Arrange
        var validator = new CreateCourseCommandValidator();
        var command = new CreateCourseCommand
        {
            Name = "Augusta National",
            Location = "Georgia",
            NumberOfHoles = 18,
            TotalPar = 72,
            CourseRating = 76.2m,
            SlopeRating = 137
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
