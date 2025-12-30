using DailyJournalingApp.Api.Features.JournalEntries;
using DailyJournalingApp.Api.Features.Prompts;
using DailyJournalingApp.Api.Features.Tags;

namespace DailyJournalingApp.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void JournalEntryDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var entry = new Core.JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Title",
            Content = "Test Content",
            EntryDate = DateTime.UtcNow,
            Mood = Core.Mood.Happy,
            Tags = "Test,Tags",
            IsFavorite = true,
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
            Assert.That(dto.Mood, Is.EqualTo(entry.Mood));
            Assert.That(dto.Tags, Is.EqualTo(entry.Tags));
            Assert.That(dto.IsFavorite, Is.EqualTo(entry.IsFavorite));
        });
    }

    [Test]
    public void PromptDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var prompt = new Core.Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "Test Prompt",
            Category = "Test Category",
            IsSystemPrompt = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = prompt.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PromptId, Is.EqualTo(prompt.PromptId));
            Assert.That(dto.Text, Is.EqualTo(prompt.Text));
            Assert.That(dto.Category, Is.EqualTo(prompt.Category));
            Assert.That(dto.IsSystemPrompt, Is.EqualTo(prompt.IsSystemPrompt));
        });
    }

    [Test]
    public void TagDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var tag = new Core.Tag
        {
            TagId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Tag",
            Color = "#FF0000",
            UsageCount = 5,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = tag.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TagId, Is.EqualTo(tag.TagId));
            Assert.That(dto.UserId, Is.EqualTo(tag.UserId));
            Assert.That(dto.Name, Is.EqualTo(tag.Name));
            Assert.That(dto.Color, Is.EqualTo(tag.Color));
            Assert.That(dto.UsageCount, Is.EqualTo(tag.UsageCount));
        });
    }
}
