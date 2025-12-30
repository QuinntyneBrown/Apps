// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core.Tests;

public class SkillTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesSkill()
    {
        // Arrange
        var skillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "C# Programming";
        var category = "Programming";
        var proficiencyLevel = ProficiencyLevel.Intermediate;
        var targetLevel = ProficiencyLevel.Expert;
        var startDate = DateTime.UtcNow;
        var targetDate = DateTime.UtcNow.AddMonths(6);
        var hoursSpent = 50.5m;
        var courseIds = new List<Guid> { Guid.NewGuid() };
        var notes = "Focus on async/await patterns";

        // Act
        var skill = new Skill
        {
            SkillId = skillId,
            UserId = userId,
            Name = name,
            Category = category,
            ProficiencyLevel = proficiencyLevel,
            TargetLevel = targetLevel,
            StartDate = startDate,
            TargetDate = targetDate,
            HoursSpent = hoursSpent,
            CourseIds = courseIds,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(skill.SkillId, Is.EqualTo(skillId));
            Assert.That(skill.UserId, Is.EqualTo(userId));
            Assert.That(skill.Name, Is.EqualTo(name));
            Assert.That(skill.Category, Is.EqualTo(category));
            Assert.That(skill.ProficiencyLevel, Is.EqualTo(proficiencyLevel));
            Assert.That(skill.TargetLevel, Is.EqualTo(targetLevel));
            Assert.That(skill.StartDate, Is.EqualTo(startDate));
            Assert.That(skill.TargetDate, Is.EqualTo(targetDate));
            Assert.That(skill.HoursSpent, Is.EqualTo(hoursSpent));
            Assert.That(skill.CourseIds, Has.Count.EqualTo(1));
            Assert.That(skill.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_NewSkill_HasExpectedDefaults()
    {
        // Act
        var skill = new Skill();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(skill.Name, Is.EqualTo(string.Empty));
            Assert.That(skill.Category, Is.EqualTo(string.Empty));
            Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Beginner));
            Assert.That(skill.HoursSpent, Is.EqualTo(0));
            Assert.That(skill.CourseIds, Is.Not.Null);
            Assert.That(skill.CourseIds, Is.Empty);
            Assert.That(skill.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateProficiency_NewLevel_UpdatesLevel()
    {
        // Arrange
        var skill = new Skill
        {
            ProficiencyLevel = ProficiencyLevel.Beginner
        };

        // Act
        skill.UpdateProficiency(ProficiencyLevel.Intermediate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Intermediate));
            Assert.That(skill.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateProficiency_ProgressThroughAllLevels_UpdatesCorrectly()
    {
        // Arrange
        var skill = new Skill();

        // Act & Assert
        skill.UpdateProficiency(ProficiencyLevel.Novice);
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Novice));

        skill.UpdateProficiency(ProficiencyLevel.Intermediate);
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Intermediate));

        skill.UpdateProficiency(ProficiencyLevel.Advanced);
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Advanced));

        skill.UpdateProficiency(ProficiencyLevel.Expert);
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Expert));
    }

    [Test]
    public void AddHours_ValidHours_IncreasesHoursSpent()
    {
        // Arrange
        var skill = new Skill
        {
            HoursSpent = 10.0m
        };

        // Act
        skill.AddHours(5.5m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(skill.HoursSpent, Is.EqualTo(15.5m));
            Assert.That(skill.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AddHours_MultipleAdditions_AccumulatesCorrectly()
    {
        // Arrange
        var skill = new Skill();

        // Act
        skill.AddHours(2.0m);
        skill.AddHours(3.5m);
        skill.AddHours(1.5m);

        // Assert
        Assert.That(skill.HoursSpent, Is.EqualTo(7.0m));
    }

    [Test]
    public void AddHours_ZeroHours_UpdatesTimestamp()
    {
        // Arrange
        var skill = new Skill();

        // Act
        skill.AddHours(0);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(skill.HoursSpent, Is.EqualTo(0));
            Assert.That(skill.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void AddCourse_NewCourse_AddsCourseToList()
    {
        // Arrange
        var skill = new Skill();
        var courseId = Guid.NewGuid();

        // Act
        skill.AddCourse(courseId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(skill.CourseIds, Has.Count.EqualTo(1));
            Assert.That(skill.CourseIds, Contains.Item(courseId));
            Assert.That(skill.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AddCourse_DuplicateCourse_DoesNotAddDuplicate()
    {
        // Arrange
        var skill = new Skill();
        var courseId = Guid.NewGuid();

        // Act
        skill.AddCourse(courseId);
        skill.AddCourse(courseId);

        // Assert
        Assert.That(skill.CourseIds, Has.Count.EqualTo(1));
    }

    [Test]
    public void AddCourse_MultipleCourses_AddsAllUniqueCourses()
    {
        // Arrange
        var skill = new Skill();
        var course1 = Guid.NewGuid();
        var course2 = Guid.NewGuid();
        var course3 = Guid.NewGuid();

        // Act
        skill.AddCourse(course1);
        skill.AddCourse(course2);
        skill.AddCourse(course3);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(skill.CourseIds, Has.Count.EqualTo(3));
            Assert.That(skill.CourseIds, Contains.Item(course1));
            Assert.That(skill.CourseIds, Contains.Item(course2));
            Assert.That(skill.CourseIds, Contains.Item(course3));
        });
    }

    [Test]
    public void ProficiencyLevel_AllEnumValues_CanBeAssigned()
    {
        // Arrange
        var skill = new Skill();

        // Act & Assert
        skill.ProficiencyLevel = ProficiencyLevel.Beginner;
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Beginner));

        skill.ProficiencyLevel = ProficiencyLevel.Novice;
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Novice));

        skill.ProficiencyLevel = ProficiencyLevel.Intermediate;
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Intermediate));

        skill.ProficiencyLevel = ProficiencyLevel.Advanced;
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Advanced));

        skill.ProficiencyLevel = ProficiencyLevel.Expert;
        Assert.That(skill.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Expert));
    }

    [Test]
    public void TargetLevel_AllEnumValues_CanBeAssigned()
    {
        // Arrange
        var skill = new Skill();

        // Act & Assert
        skill.TargetLevel = ProficiencyLevel.Beginner;
        Assert.That(skill.TargetLevel, Is.EqualTo(ProficiencyLevel.Beginner));

        skill.TargetLevel = ProficiencyLevel.Expert;
        Assert.That(skill.TargetLevel, Is.EqualTo(ProficiencyLevel.Expert));
    }

    [Test]
    public void TargetLevel_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var skill = new Skill
        {
            TargetLevel = null
        };

        // Assert
        Assert.That(skill.TargetLevel, Is.Null);
    }

    [Test]
    public void TargetDate_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var skill = new Skill
        {
            TargetDate = null
        };

        // Assert
        Assert.That(skill.TargetDate, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var skill = new Skill
        {
            Notes = null
        };

        // Assert
        Assert.That(skill.Notes, Is.Null);
    }
}
