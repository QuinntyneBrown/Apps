using SkillDevelopmentTracker.Api.Features.Courses;
using SkillDevelopmentTracker.Core;

namespace SkillDevelopmentTracker.Api.Tests.Features.Courses;

[TestFixture]
public class CourseDtoTests
{
    [Test]
    public void ToDto_MapsAllProperties_Correctly()
    {
        // Arrange
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Advanced React Patterns",
            Provider = "Frontend Masters",
            Instructor = "Kent C. Dodds",
            CourseUrl = "https://frontendmasters.com/courses/advanced-react-patterns/",
            StartDate = new DateTime(2024, 1, 10),
            CompletionDate = new DateTime(2024, 2, 15),
            ProgressPercentage = 100,
            EstimatedHours = 8m,
            ActualHours = 10m,
            IsCompleted = true,
            SkillIds = new List<Guid> { Guid.NewGuid() },
            Notes = "Great course!",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = course.ToDto();

        // Assert
        Assert.That(dto.CourseId, Is.EqualTo(course.CourseId));
        Assert.That(dto.UserId, Is.EqualTo(course.UserId));
        Assert.That(dto.Title, Is.EqualTo(course.Title));
        Assert.That(dto.Provider, Is.EqualTo(course.Provider));
        Assert.That(dto.Instructor, Is.EqualTo(course.Instructor));
        Assert.That(dto.CourseUrl, Is.EqualTo(course.CourseUrl));
        Assert.That(dto.StartDate, Is.EqualTo(course.StartDate));
        Assert.That(dto.CompletionDate, Is.EqualTo(course.CompletionDate));
        Assert.That(dto.ProgressPercentage, Is.EqualTo(course.ProgressPercentage));
        Assert.That(dto.EstimatedHours, Is.EqualTo(course.EstimatedHours));
        Assert.That(dto.ActualHours, Is.EqualTo(course.ActualHours));
        Assert.That(dto.IsCompleted, Is.EqualTo(course.IsCompleted));
        Assert.That(dto.SkillIds, Is.EqualTo(course.SkillIds));
        Assert.That(dto.Notes, Is.EqualTo(course.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(course.CreatedAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(course.UpdatedAt));
    }
}
