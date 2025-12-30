// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core.Tests;

public class CourseTests
{
    [Test]
    public void Course_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var name = "Pebble Beach";
        var location = "California";
        var numberOfHoles = 18;
        var totalPar = 72;
        var courseRating = 75.5m;
        var slopeRating = 145;
        var notes = "Famous coastal course";
        var createdAt = DateTime.UtcNow;

        // Act
        var course = new Course
        {
            CourseId = courseId,
            Name = name,
            Location = location,
            NumberOfHoles = numberOfHoles,
            TotalPar = totalPar,
            CourseRating = courseRating,
            SlopeRating = slopeRating,
            Notes = notes,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.CourseId, Is.EqualTo(courseId));
            Assert.That(course.Name, Is.EqualTo(name));
            Assert.That(course.Location, Is.EqualTo(location));
            Assert.That(course.NumberOfHoles, Is.EqualTo(numberOfHoles));
            Assert.That(course.TotalPar, Is.EqualTo(totalPar));
            Assert.That(course.CourseRating, Is.EqualTo(courseRating));
            Assert.That(course.SlopeRating, Is.EqualTo(slopeRating));
            Assert.That(course.Notes, Is.EqualTo(notes));
            Assert.That(course.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Course_DefaultName_IsEmptyString()
    {
        // Arrange & Act
        var course = new Course();

        // Assert
        Assert.That(course.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Course_DefaultNumberOfHoles_Is18()
    {
        // Arrange & Act
        var course = new Course();

        // Assert
        Assert.That(course.NumberOfHoles, Is.EqualTo(18));
    }

    [Test]
    public void Course_Location_CanBeNull()
    {
        // Arrange & Act
        var course = new Course
        {
            Location = null
        };

        // Assert
        Assert.That(course.Location, Is.Null);
    }

    [Test]
    public void Course_CourseRating_CanBeNull()
    {
        // Arrange & Act
        var course = new Course
        {
            CourseRating = null
        };

        // Assert
        Assert.That(course.CourseRating, Is.Null);
    }

    [Test]
    public void Course_SlopeRating_CanBeNull()
    {
        // Arrange & Act
        var course = new Course
        {
            SlopeRating = null
        };

        // Assert
        Assert.That(course.SlopeRating, Is.Null);
    }

    [Test]
    public void Course_Notes_CanBeNull()
    {
        // Arrange & Act
        var course = new Course
        {
            Notes = null
        };

        // Assert
        Assert.That(course.Notes, Is.Null);
    }

    [Test]
    public void Course_Rounds_DefaultsToEmptyList()
    {
        // Arrange & Act
        var course = new Course();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.Rounds, Is.Not.Null);
            Assert.That(course.Rounds, Is.Empty);
        });
    }

    [Test]
    public void IsFullCourse_ReturnsTrue_When18Holes()
    {
        // Arrange
        var course = new Course
        {
            NumberOfHoles = 18
        };

        // Act
        var result = course.IsFullCourse();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsFullCourse_ReturnsFalse_When9Holes()
    {
        // Arrange
        var course = new Course
        {
            NumberOfHoles = 9
        };

        // Act
        var result = course.IsFullCourse();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFullCourse_ReturnsFalse_WhenNotStandard()
    {
        // Arrange
        var course = new Course
        {
            NumberOfHoles = 27
        };

        // Act
        var result = course.IsFullCourse();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Course_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var course = new Course();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(course.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Course_CanHaveMultipleRounds()
    {
        // Arrange
        var course = new Course();
        var round1 = new Round { TotalScore = 85 };
        var round2 = new Round { TotalScore = 78 };

        // Act
        course.Rounds.Add(round1);
        course.Rounds.Add(round2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.Rounds, Has.Count.EqualTo(2));
            Assert.That(course.Rounds, Contains.Item(round1));
            Assert.That(course.Rounds, Contains.Item(round2));
        });
    }

    [Test]
    public void Course_AllProperties_CanBeModified()
    {
        // Arrange
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Name = "Initial Name"
        };

        var newCourseId = Guid.NewGuid();

        // Act
        course.CourseId = newCourseId;
        course.Name = "Updated Name";
        course.Location = "New Location";
        course.NumberOfHoles = 9;
        course.TotalPar = 36;
        course.CourseRating = 70.5m;
        course.SlopeRating = 125;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.CourseId, Is.EqualTo(newCourseId));
            Assert.That(course.Name, Is.EqualTo("Updated Name"));
            Assert.That(course.Location, Is.EqualTo("New Location"));
            Assert.That(course.NumberOfHoles, Is.EqualTo(9));
            Assert.That(course.TotalPar, Is.EqualTo(36));
            Assert.That(course.CourseRating, Is.EqualTo(70.5m));
            Assert.That(course.SlopeRating, Is.EqualTo(125));
        });
    }
}
