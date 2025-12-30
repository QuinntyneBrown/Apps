// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentAssertions;
using GolfScoreTracker.Api.Controllers;
using GolfScoreTracker.Api.Features.Courses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GolfScoreTracker.Api.Tests;

public class CoursesControllerTests
{
    private Mock<IMediator> _mockMediator;
    private CoursesController _controller;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new CoursesController(_mockMediator.Object);
    }

    [Test]
    public async Task GetCourses_ShouldReturnOkResultWithCourses()
    {
        // Arrange
        var courses = new List<CourseDto>
        {
            new CourseDto
            {
                CourseId = Guid.NewGuid(),
                Name = "Pebble Beach",
                Location = "California",
                NumberOfHoles = 18,
                TotalPar = 72
            }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetCoursesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        // Act
        var result = await _controller.GetCourses();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(courses);
    }

    [Test]
    public async Task GetCourseById_ShouldReturnOkResultWhenCourseExists()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var course = new CourseDto
        {
            CourseId = courseId,
            Name = "Augusta National",
            Location = "Georgia",
            NumberOfHoles = 18,
            TotalPar = 72
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetCourseByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(course);

        // Act
        var result = await _controller.GetCourseById(courseId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(course);
    }

    [Test]
    public async Task GetCourseById_ShouldReturnNotFoundWhenCourseDoesNotExist()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<GetCourseByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CourseDto?)null);

        // Act
        var result = await _controller.GetCourseById(courseId);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task CreateCourse_ShouldReturnCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            Name = "St Andrews",
            Location = "Scotland",
            NumberOfHoles = 18,
            TotalPar = 72
        };

        var createdCourse = new CourseDto
        {
            CourseId = Guid.NewGuid(),
            Name = command.Name,
            Location = command.Location,
            NumberOfHoles = command.NumberOfHoles,
            TotalPar = command.TotalPar
        };

        _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCourse);

        // Act
        var result = await _controller.CreateCourse(command);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult!.Value.Should().BeEquivalentTo(createdCourse);
    }

    [Test]
    public async Task DeleteCourse_ShouldReturnNoContentWhenCourseExists()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteCourseCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteCourse(courseId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task DeleteCourse_ShouldReturnNotFoundWhenCourseDoesNotExist()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteCourseCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteCourse(courseId);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
