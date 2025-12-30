// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core.Tests;

public class LearningPathTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLearningPath()
    {
        // Arrange
        var pathId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Full Stack Developer Path";
        var description = "Complete learning path for full stack development";
        var startDate = DateTime.UtcNow;
        var targetDate = DateTime.UtcNow.AddMonths(6);
        var courseIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var skillIds = new List<Guid> { Guid.NewGuid() };

        // Act
        var learningPath = new LearningPath
        {
            LearningPathId = pathId,
            UserId = userId,
            Title = title,
            Description = description,
            StartDate = startDate,
            TargetDate = targetDate,
            CourseIds = courseIds,
            SkillIds = skillIds
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.LearningPathId, Is.EqualTo(pathId));
            Assert.That(learningPath.UserId, Is.EqualTo(userId));
            Assert.That(learningPath.Title, Is.EqualTo(title));
            Assert.That(learningPath.Description, Is.EqualTo(description));
            Assert.That(learningPath.StartDate, Is.EqualTo(startDate));
            Assert.That(learningPath.TargetDate, Is.EqualTo(targetDate));
            Assert.That(learningPath.CourseIds, Has.Count.EqualTo(2));
            Assert.That(learningPath.SkillIds, Has.Count.EqualTo(1));
            Assert.That(learningPath.ProgressPercentage, Is.EqualTo(0));
            Assert.That(learningPath.IsCompleted, Is.False);
        });
    }

    [Test]
    public void DefaultValues_NewLearningPath_HasExpectedDefaults()
    {
        // Act
        var learningPath = new LearningPath();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.Title, Is.EqualTo(string.Empty));
            Assert.That(learningPath.Description, Is.EqualTo(string.Empty));
            Assert.That(learningPath.ProgressPercentage, Is.EqualTo(0));
            Assert.That(learningPath.IsCompleted, Is.False);
            Assert.That(learningPath.CourseIds, Is.Not.Null);
            Assert.That(learningPath.CourseIds, Is.Empty);
            Assert.That(learningPath.SkillIds, Is.Not.Null);
            Assert.That(learningPath.SkillIds, Is.Empty);
            Assert.That(learningPath.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateProgress_ValidPercentage_UpdatesProgress()
    {
        // Arrange
        var learningPath = new LearningPath();

        // Act
        learningPath.UpdateProgress(60);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.ProgressPercentage, Is.EqualTo(60));
            Assert.That(learningPath.IsCompleted, Is.False);
            Assert.That(learningPath.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateProgress_100Percent_AutomaticallyCompletesPath()
    {
        // Arrange
        var learningPath = new LearningPath();

        // Act
        learningPath.UpdateProgress(100);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.ProgressPercentage, Is.EqualTo(100));
            Assert.That(learningPath.IsCompleted, Is.True);
            Assert.That(learningPath.CompletionDate, Is.Not.Null);
            Assert.That(learningPath.CompletionDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateProgress_PercentageAbove100_ClampsTo100()
    {
        // Arrange
        var learningPath = new LearningPath();

        // Act
        learningPath.UpdateProgress(200);

        // Assert
        Assert.That(learningPath.ProgressPercentage, Is.EqualTo(100));
    }

    [Test]
    public void UpdateProgress_NegativePercentage_ClampsTo0()
    {
        // Arrange
        var learningPath = new LearningPath();

        // Act
        learningPath.UpdateProgress(-50);

        // Assert
        Assert.That(learningPath.ProgressPercentage, Is.EqualTo(0));
    }

    [Test]
    public void UpdateProgress_AlreadyCompleted_DoesNotCallCompleteAgain()
    {
        // Arrange
        var learningPath = new LearningPath();
        learningPath.Complete();
        var firstCompletionDate = learningPath.CompletionDate;

        // Act
        Thread.Sleep(10); // Ensure time difference
        learningPath.UpdateProgress(100);

        // Assert
        Assert.That(learningPath.CompletionDate, Is.EqualTo(firstCompletionDate));
    }

    [Test]
    public void Complete_NotCompleted_MarksPathAsCompleted()
    {
        // Arrange
        var learningPath = new LearningPath
        {
            ProgressPercentage = 80
        };

        // Act
        learningPath.Complete();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.IsCompleted, Is.True);
            Assert.That(learningPath.ProgressPercentage, Is.EqualTo(100));
            Assert.That(learningPath.CompletionDate, Is.Not.Null);
            Assert.That(learningPath.CompletionDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(learningPath.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AddCourse_NewCourse_AddsCourseToList()
    {
        // Arrange
        var learningPath = new LearningPath();
        var courseId = Guid.NewGuid();

        // Act
        learningPath.AddCourse(courseId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.CourseIds, Has.Count.EqualTo(1));
            Assert.That(learningPath.CourseIds, Contains.Item(courseId));
            Assert.That(learningPath.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AddCourse_DuplicateCourse_DoesNotAddDuplicate()
    {
        // Arrange
        var learningPath = new LearningPath();
        var courseId = Guid.NewGuid();

        // Act
        learningPath.AddCourse(courseId);
        learningPath.AddCourse(courseId);

        // Assert
        Assert.That(learningPath.CourseIds, Has.Count.EqualTo(1));
    }

    [Test]
    public void AddCourse_MultipleCourses_AddsAllUniqueCourses()
    {
        // Arrange
        var learningPath = new LearningPath();
        var course1 = Guid.NewGuid();
        var course2 = Guid.NewGuid();
        var course3 = Guid.NewGuid();

        // Act
        learningPath.AddCourse(course1);
        learningPath.AddCourse(course2);
        learningPath.AddCourse(course3);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.CourseIds, Has.Count.EqualTo(3));
            Assert.That(learningPath.CourseIds, Contains.Item(course1));
            Assert.That(learningPath.CourseIds, Contains.Item(course2));
            Assert.That(learningPath.CourseIds, Contains.Item(course3));
        });
    }

    [Test]
    public void AddSkill_NewSkill_AddsSkillToList()
    {
        // Arrange
        var learningPath = new LearningPath();
        var skillId = Guid.NewGuid();

        // Act
        learningPath.AddSkill(skillId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.SkillIds, Has.Count.EqualTo(1));
            Assert.That(learningPath.SkillIds, Contains.Item(skillId));
            Assert.That(learningPath.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AddSkill_DuplicateSkill_DoesNotAddDuplicate()
    {
        // Arrange
        var learningPath = new LearningPath();
        var skillId = Guid.NewGuid();

        // Act
        learningPath.AddSkill(skillId);
        learningPath.AddSkill(skillId);

        // Assert
        Assert.That(learningPath.SkillIds, Has.Count.EqualTo(1));
    }

    [Test]
    public void AddSkill_MultipleSkills_AddsAllUniqueSkills()
    {
        // Arrange
        var learningPath = new LearningPath();
        var skill1 = Guid.NewGuid();
        var skill2 = Guid.NewGuid();

        // Act
        learningPath.AddSkill(skill1);
        learningPath.AddSkill(skill2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(learningPath.SkillIds, Has.Count.EqualTo(2));
            Assert.That(learningPath.SkillIds, Contains.Item(skill1));
            Assert.That(learningPath.SkillIds, Contains.Item(skill2));
        });
    }

    [Test]
    public void TargetDate_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var learningPath = new LearningPath
        {
            TargetDate = null
        };

        // Assert
        Assert.That(learningPath.TargetDate, Is.Null);
    }

    [Test]
    public void CompletionDate_OptionalField_InitiallyNull()
    {
        // Arrange & Act
        var learningPath = new LearningPath();

        // Assert
        Assert.That(learningPath.CompletionDate, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var learningPath = new LearningPath
        {
            Notes = null
        };

        // Assert
        Assert.That(learningPath.Notes, Is.Null);
    }
}
