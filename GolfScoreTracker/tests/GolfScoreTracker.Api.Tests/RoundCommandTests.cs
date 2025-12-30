// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentAssertions;
using GolfScoreTracker.Api.Features.Rounds;
using GolfScoreTracker.Core;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GolfScoreTracker.Api.Tests;

public class RoundCommandTests
{
    private Mock<IGolfScoreTrackerContext> _mockContext;
    private Mock<DbSet<Round>> _mockRoundSet;
    private Mock<DbSet<Course>> _mockCourseSet;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IGolfScoreTrackerContext>();
        _mockRoundSet = new Mock<DbSet<Round>>();
        _mockCourseSet = new Mock<DbSet<Course>>();
    }

    [Test]
    public void CreateRoundCommandValidator_ShouldFailWhenUserIdIsEmpty()
    {
        // Arrange
        var validator = new CreateRoundCommandValidator();
        var command = new CreateRoundCommand
        {
            UserId = Guid.Empty,
            CourseId = Guid.NewGuid(),
            PlayedDate = DateTime.UtcNow,
            TotalScore = 85,
            TotalPar = 72
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UserId");
    }

    [Test]
    public void CreateRoundCommandValidator_ShouldFailWhenPlayedDateIsInFuture()
    {
        // Arrange
        var validator = new CreateRoundCommandValidator();
        var command = new CreateRoundCommand
        {
            UserId = Guid.NewGuid(),
            CourseId = Guid.NewGuid(),
            PlayedDate = DateTime.UtcNow.AddDays(1),
            TotalScore = 85,
            TotalPar = 72
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PlayedDate");
    }

    [Test]
    public void CreateRoundCommandValidator_ShouldPassWithValidData()
    {
        // Arrange
        var validator = new CreateRoundCommandValidator();
        var command = new CreateRoundCommand
        {
            UserId = Guid.NewGuid(),
            CourseId = Guid.NewGuid(),
            PlayedDate = DateTime.UtcNow.AddDays(-1),
            TotalScore = 85,
            TotalPar = 72,
            Weather = "Sunny",
            Notes = "Great round!"
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
