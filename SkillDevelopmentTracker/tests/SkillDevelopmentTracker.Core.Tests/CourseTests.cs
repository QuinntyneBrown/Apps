// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core.Tests;

public class CourseTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCourse()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "C# Advanced Programming";
        var provider = "Pluralsight";
        var instructor = "John Doe";
        var courseUrl = "https://pluralsight.com/course/csharp-advanced";
        var startDate = DateTime.UtcNow;
        var estimatedHours = 20.5m;
        var skillIds = new List<Guid> { Guid.NewGuid() };

        // Act
        var course = new Course
        {
            CourseId = courseId,
            UserId = userId,
            Title = title,
            Provider = provider,
            Instructor = instructor,
            CourseUrl = courseUrl,
            StartDate = startDate,
            EstimatedHours = estimatedHours,
            SkillIds = skillIds
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.CourseId, Is.EqualTo(courseId));
            Assert.That(course.UserId, Is.EqualTo(userId));
            Assert.That(course.Title, Is.EqualTo(title));
            Assert.That(course.Provider, Is.EqualTo(provider));
            Assert.That(course.Instructor, Is.EqualTo(instructor));
            Assert.That(course.CourseUrl, Is.EqualTo(courseUrl));
            Assert.That(course.StartDate, Is.EqualTo(startDate));
            Assert.That(course.EstimatedHours, Is.EqualTo(estimatedHours));
            Assert.That(course.SkillIds, Has.Count.EqualTo(1));
            Assert.That(course.ProgressPercentage, Is.EqualTo(0));
            Assert.That(course.IsCompleted, Is.False);
        });
    }

    [Test]
    public void DefaultValues_NewCourse_HasExpectedDefaults()
    {
        // Act
        var course = new Course();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.Title, Is.EqualTo(string.Empty));
            Assert.That(course.Provider, Is.EqualTo(string.Empty));
            Assert.That(course.ProgressPercentage, Is.EqualTo(0));
            Assert.That(course.ActualHours, Is.EqualTo(0));
            Assert.That(course.IsCompleted, Is.False);
            Assert.That(course.SkillIds, Is.Not.Null);
            Assert.That(course.SkillIds, Is.Empty);
            Assert.That(course.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateProgress_ValidPercentage_UpdatesProgress()
    {
        // Arrange
        var course = new Course();

        // Act
        course.UpdateProgress(50);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.ProgressPercentage, Is.EqualTo(50));
            Assert.That(course.IsCompleted, Is.False);
            Assert.That(course.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateProgress_100Percent_AutomaticallyCompletesCourse()
    {
        // Arrange
        var course = new Course();

        // Act
        course.UpdateProgress(100);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.ProgressPercentage, Is.EqualTo(100));
            Assert.That(course.IsCompleted, Is.True);
            Assert.That(course.CompletionDate, Is.Not.Null);
            Assert.That(course.CompletionDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateProgress_PercentageAbove100_ClampsTo100()
    {
        // Arrange
        var course = new Course();

        // Act
        course.UpdateProgress(150);

        // Assert
        Assert.That(course.ProgressPercentage, Is.EqualTo(100));
    }

    [Test]
    public void UpdateProgress_NegativePercentage_ClampsTo0()
    {
        // Arrange
        var course = new Course();

        // Act
        course.UpdateProgress(-10);

        // Assert
        Assert.That(course.ProgressPercentage, Is.EqualTo(0));
    }

    [Test]
    public void UpdateProgress_AlreadyCompleted_DoesNotCallCompleteAgain()
    {
        // Arrange
        var course = new Course();
        course.Complete();
        var firstCompletionDate = course.CompletionDate;

        // Act
        Thread.Sleep(10); // Ensure time difference
        course.UpdateProgress(100);

        // Assert
        Assert.That(course.CompletionDate, Is.EqualTo(firstCompletionDate));
    }

    [Test]
    public void Complete_NotCompleted_MarksCourseAsCompleted()
    {
        // Arrange
        var course = new Course
        {
            ProgressPercentage = 75
        };

        // Act
        course.Complete();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.IsCompleted, Is.True);
            Assert.That(course.ProgressPercentage, Is.EqualTo(100));
            Assert.That(course.CompletionDate, Is.Not.Null);
            Assert.That(course.CompletionDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(course.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AddHours_ValidHours_IncreasesActualHours()
    {
        // Arrange
        var course = new Course
        {
            ActualHours = 5.5m
        };

        // Act
        course.AddHours(2.5m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.ActualHours, Is.EqualTo(8.0m));
            Assert.That(course.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AddHours_MultipleAdditions_AccumulatesCorrectly()
    {
        // Arrange
        var course = new Course();

        // Act
        course.AddHours(1.5m);
        course.AddHours(2.0m);
        course.AddHours(3.5m);

        // Assert
        Assert.That(course.ActualHours, Is.EqualTo(7.0m));
    }

    [Test]
    public void AddHours_ZeroHours_UpdatesTimestamp()
    {
        // Arrange
        var course = new Course();

        // Act
        course.AddHours(0);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.ActualHours, Is.EqualTo(0));
            Assert.That(course.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void SkillIds_AddMultipleSkills_StoresAllSkills()
    {
        // Arrange
        var course = new Course();
        var skill1 = Guid.NewGuid();
        var skill2 = Guid.NewGuid();

        // Act
        course.SkillIds.Add(skill1);
        course.SkillIds.Add(skill2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.SkillIds, Has.Count.EqualTo(2));
            Assert.That(course.SkillIds, Contains.Item(skill1));
            Assert.That(course.SkillIds, Contains.Item(skill2));
        });
    }

    [Test]
    public void Instructor_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var course = new Course
        {
            Instructor = null
        };

        // Assert
        Assert.That(course.Instructor, Is.Null);
    }

    [Test]
    public void CourseUrl_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var course = new Course
        {
            CourseUrl = null
        };

        // Assert
        Assert.That(course.CourseUrl, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var course = new Course
        {
            Notes = null
        };

        // Assert
        Assert.That(course.Notes, Is.Null);
    }
}
