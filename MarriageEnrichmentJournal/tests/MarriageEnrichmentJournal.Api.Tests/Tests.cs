using MarriageEnrichmentJournal.Api.Features.JournalEntries;
using MarriageEnrichmentJournal.Api.Features.Gratitudes;
using MarriageEnrichmentJournal.Api.Features.Reflections;

namespace MarriageEnrichmentJournal.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void JournalEntryDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Title",
            Content = "Test Content",
            EntryType = EntryType.Gratitude,
            EntryDate = DateTime.UtcNow,
            IsSharedWithPartner = true,
            IsPrivate = false,
            Tags = "Test,Tags",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = entry.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.JournalEntryId, Is.EqualTo(entry.JournalEntryId));
            Assert.That(dto.UserId, Is.EqualTo(entry.UserId));
            Assert.That(dto.Title, Is.EqualTo(entry.Title));
            Assert.That(dto.Content, Is.EqualTo(entry.Content));
            Assert.That(dto.EntryType, Is.EqualTo(entry.EntryType));
            Assert.That(dto.EntryDate, Is.EqualTo(entry.EntryDate));
            Assert.That(dto.IsSharedWithPartner, Is.EqualTo(entry.IsSharedWithPartner));
            Assert.That(dto.IsPrivate, Is.EqualTo(entry.IsPrivate));
            Assert.That(dto.Tags, Is.EqualTo(entry.Tags));
            Assert.That(dto.CreatedAt, Is.EqualTo(entry.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(entry.UpdatedAt));
        });
    }

    [Test]
    public void GratitudeDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var gratitude = new Gratitude
        {
            GratitudeId = Guid.NewGuid(),
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Text = "I am grateful for my partner",
            GratitudeDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = gratitude.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GratitudeId, Is.EqualTo(gratitude.GratitudeId));
            Assert.That(dto.JournalEntryId, Is.EqualTo(gratitude.JournalEntryId));
            Assert.That(dto.UserId, Is.EqualTo(gratitude.UserId));
            Assert.That(dto.Text, Is.EqualTo(gratitude.Text));
            Assert.That(dto.GratitudeDate, Is.EqualTo(gratitude.GratitudeDate));
            Assert.That(dto.CreatedAt, Is.EqualTo(gratitude.CreatedAt));
        });
    }

    [Test]
    public void ReflectionDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var reflection = new Reflection
        {
            ReflectionId = Guid.NewGuid(),
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Text = "Reflecting on our communication",
            Topic = "Communication",
            ReflectionDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = reflection.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ReflectionId, Is.EqualTo(reflection.ReflectionId));
            Assert.That(dto.JournalEntryId, Is.EqualTo(reflection.JournalEntryId));
            Assert.That(dto.UserId, Is.EqualTo(reflection.UserId));
            Assert.That(dto.Text, Is.EqualTo(reflection.Text));
            Assert.That(dto.Topic, Is.EqualTo(reflection.Topic));
            Assert.That(dto.ReflectionDate, Is.EqualTo(reflection.ReflectionDate));
            Assert.That(dto.CreatedAt, Is.EqualTo(reflection.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(reflection.UpdatedAt));
        });
    }
}
