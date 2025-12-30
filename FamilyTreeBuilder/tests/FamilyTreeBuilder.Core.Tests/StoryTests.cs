// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class StoryTests
{
    [Test]
    public void Constructor_CreatesStory_WithDefaultValues()
    {
        // Arrange & Act
        var story = new Story();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(story.StoryId, Is.EqualTo(Guid.Empty));
            Assert.That(story.PersonId, Is.EqualTo(Guid.Empty));
            Assert.That(story.Person, Is.Null);
            Assert.That(story.Title, Is.EqualTo(string.Empty));
            Assert.That(story.Content, Is.Null);
            Assert.That(story.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void StoryId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var story = new Story();
        var expectedId = Guid.NewGuid();

        // Act
        story.StoryId = expectedId;

        // Assert
        Assert.That(story.StoryId, Is.EqualTo(expectedId));
    }

    [Test]
    public void PersonId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var story = new Story();
        var expectedPersonId = Guid.NewGuid();

        // Act
        story.PersonId = expectedPersonId;

        // Assert
        Assert.That(story.PersonId, Is.EqualTo(expectedPersonId));
    }

    [Test]
    public void Title_CanBeSet_AndRetrieved()
    {
        // Arrange
        var story = new Story();
        var expectedTitle = "Immigration to America";

        // Act
        story.Title = expectedTitle;

        // Assert
        Assert.That(story.Title, Is.EqualTo(expectedTitle));
    }

    [Test]
    public void Content_CanBeSet_AndRetrieved()
    {
        // Arrange
        var story = new Story();
        var expectedContent = "In 1920, my grandfather arrived at Ellis Island...";

        // Act
        story.Content = expectedContent;

        // Assert
        Assert.That(story.Content, Is.EqualTo(expectedContent));
    }

    [Test]
    public void Content_CanBeNull()
    {
        // Arrange
        var story = new Story();

        // Act
        story.Content = null;

        // Assert
        Assert.That(story.Content, Is.Null);
    }

    [Test]
    public void Story_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var storyId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var title = "War Service";
        var content = "During World War II, he served as a medic...";
        var createdAt = DateTime.UtcNow;

        // Act
        var story = new Story
        {
            StoryId = storyId,
            PersonId = personId,
            Title = title,
            Content = content,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(story.StoryId, Is.EqualTo(storyId));
            Assert.That(story.PersonId, Is.EqualTo(personId));
            Assert.That(story.Title, Is.EqualTo(title));
            Assert.That(story.Content, Is.EqualTo(content));
            Assert.That(story.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Story_WithoutContent_CanBeCreated()
    {
        // Arrange
        var storyId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var title = "Early Life";

        // Act
        var story = new Story
        {
            StoryId = storyId,
            PersonId = personId,
            Title = title
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(story.StoryId, Is.EqualTo(storyId));
            Assert.That(story.PersonId, Is.EqualTo(personId));
            Assert.That(story.Title, Is.EqualTo(title));
            Assert.That(story.Content, Is.Null);
        });
    }
}
