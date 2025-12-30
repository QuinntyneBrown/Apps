using ProfessionalReadingList.Api.Features.Resources;
using ProfessionalReadingList.Api.Features.Notes;
using ProfessionalReadingList.Api.Features.ReadingProgress;

namespace ProfessionalReadingList.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void ResourceDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var resource = new Core.Resource
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Clean Code",
            ResourceType = Core.ResourceType.Book,
            Author = "Robert C. Martin",
            Publisher = "Prentice Hall",
            PublicationDate = new DateTime(2008, 8, 1),
            Url = "https://example.com/clean-code",
            Isbn = "978-0132350884",
            TotalPages = 464,
            Topics = new List<string> { "Software Engineering", "Best Practices" },
            DateAdded = DateTime.UtcNow,
            Notes = "Must read for developers",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = resource.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ResourceId, Is.EqualTo(resource.ResourceId));
            Assert.That(dto.UserId, Is.EqualTo(resource.UserId));
            Assert.That(dto.Title, Is.EqualTo(resource.Title));
            Assert.That(dto.ResourceType, Is.EqualTo(resource.ResourceType));
            Assert.That(dto.Author, Is.EqualTo(resource.Author));
            Assert.That(dto.Publisher, Is.EqualTo(resource.Publisher));
            Assert.That(dto.PublicationDate, Is.EqualTo(resource.PublicationDate));
            Assert.That(dto.Url, Is.EqualTo(resource.Url));
            Assert.That(dto.Isbn, Is.EqualTo(resource.Isbn));
            Assert.That(dto.TotalPages, Is.EqualTo(resource.TotalPages));
            Assert.That(dto.Topics, Is.EqualTo(resource.Topics));
            Assert.That(dto.DateAdded, Is.EqualTo(resource.DateAdded));
            Assert.That(dto.Notes, Is.EqualTo(resource.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(resource.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(resource.UpdatedAt));
        });
    }

    [Test]
    public void NoteDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var note = new Core.Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            Content = "This is an important concept about clean code",
            PageReference = "Page 42",
            Quote = "Clean code is simple and direct",
            Tags = new List<string> { "Clean Code", "Best Practices" },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = note.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.NoteId, Is.EqualTo(note.NoteId));
            Assert.That(dto.UserId, Is.EqualTo(note.UserId));
            Assert.That(dto.ResourceId, Is.EqualTo(note.ResourceId));
            Assert.That(dto.Content, Is.EqualTo(note.Content));
            Assert.That(dto.PageReference, Is.EqualTo(note.PageReference));
            Assert.That(dto.Quote, Is.EqualTo(note.Quote));
            Assert.That(dto.Tags, Is.EqualTo(note.Tags));
            Assert.That(dto.CreatedAt, Is.EqualTo(note.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(note.UpdatedAt));
        });
    }

    [Test]
    public void ReadingProgressDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var readingProgress = new Core.ReadingProgress
        {
            ReadingProgressId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            Status = "Reading",
            CurrentPage = 150,
            ProgressPercentage = 32,
            StartDate = DateTime.UtcNow.AddDays(-5),
            CompletionDate = null,
            Rating = null,
            Review = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = readingProgress.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ReadingProgressId, Is.EqualTo(readingProgress.ReadingProgressId));
            Assert.That(dto.UserId, Is.EqualTo(readingProgress.UserId));
            Assert.That(dto.ResourceId, Is.EqualTo(readingProgress.ResourceId));
            Assert.That(dto.Status, Is.EqualTo(readingProgress.Status));
            Assert.That(dto.CurrentPage, Is.EqualTo(readingProgress.CurrentPage));
            Assert.That(dto.ProgressPercentage, Is.EqualTo(readingProgress.ProgressPercentage));
            Assert.That(dto.StartDate, Is.EqualTo(readingProgress.StartDate));
            Assert.That(dto.CompletionDate, Is.EqualTo(readingProgress.CompletionDate));
            Assert.That(dto.Rating, Is.EqualTo(readingProgress.Rating));
            Assert.That(dto.Review, Is.EqualTo(readingProgress.Review));
            Assert.That(dto.CreatedAt, Is.EqualTo(readingProgress.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(readingProgress.UpdatedAt));
        });
    }
}
