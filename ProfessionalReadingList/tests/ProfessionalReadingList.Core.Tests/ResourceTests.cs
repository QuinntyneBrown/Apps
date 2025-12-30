// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core.Tests;

public class ResourceTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesResource()
    {
        // Arrange & Act
        var resource = new Resource();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resource.ResourceId, Is.EqualTo(Guid.Empty));
            Assert.That(resource.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(resource.Title, Is.EqualTo(string.Empty));
            Assert.That(resource.ResourceType, Is.EqualTo(ResourceType.Book));
            Assert.That(resource.Author, Is.Null);
            Assert.That(resource.Publisher, Is.Null);
            Assert.That(resource.PublicationDate, Is.Null);
            Assert.That(resource.Url, Is.Null);
            Assert.That(resource.Isbn, Is.Null);
            Assert.That(resource.TotalPages, Is.Null);
            Assert.That(resource.Topics, Is.Not.Null);
            Assert.That(resource.Topics, Is.Empty);
            Assert.That(resource.DateAdded, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(resource.Notes, Is.Null);
            Assert.That(resource.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(resource.UpdatedAt, Is.Null);
            Assert.That(resource.ReadingProgress, Is.Null);
            Assert.That(resource.ResourceNotes, Is.Not.Null);
            Assert.That(resource.ResourceNotes, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesResource()
    {
        // Arrange
        var resourceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Clean Code";
        var author = "Robert C. Martin";
        var publisher = "Prentice Hall";
        var isbn = "978-0132350884";

        // Act
        var resource = new Resource
        {
            ResourceId = resourceId,
            UserId = userId,
            Title = title,
            ResourceType = ResourceType.Book,
            Author = author,
            Publisher = publisher,
            Isbn = isbn,
            TotalPages = 464
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resource.ResourceId, Is.EqualTo(resourceId));
            Assert.That(resource.UserId, Is.EqualTo(userId));
            Assert.That(resource.Title, Is.EqualTo(title));
            Assert.That(resource.ResourceType, Is.EqualTo(ResourceType.Book));
            Assert.That(resource.Author, Is.EqualTo(author));
            Assert.That(resource.Publisher, Is.EqualTo(publisher));
            Assert.That(resource.Isbn, Is.EqualTo(isbn));
            Assert.That(resource.TotalPages, Is.EqualTo(464));
        });
    }

    [Test]
    public void Resource_BookType_SetsCorrectly()
    {
        // Arrange & Act
        var resource = new Resource
        {
            Title = "The Pragmatic Programmer",
            ResourceType = ResourceType.Book,
            Author = "Andrew Hunt and David Thomas",
            Isbn = "978-0135957059"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resource.ResourceType, Is.EqualTo(ResourceType.Book));
            Assert.That(resource.Isbn, Is.Not.Null);
        });
    }

    [Test]
    public void Resource_ArticleType_SetsCorrectly()
    {
        // Arrange & Act
        var resource = new Resource
        {
            Title = "Introduction to Microservices",
            ResourceType = ResourceType.Article,
            Url = "https://example.com/microservices"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resource.ResourceType, Is.EqualTo(ResourceType.Article));
            Assert.That(resource.Url, Is.Not.Null);
        });
    }

    [Test]
    public void Resource_VideoType_SetsCorrectly()
    {
        // Arrange & Act
        var resource = new Resource
        {
            Title = "Advanced C# Course",
            ResourceType = ResourceType.Video,
            Url = "https://example.com/csharp-course"
        };

        // Assert
        Assert.That(resource.ResourceType, Is.EqualTo(ResourceType.Video));
    }

    [Test]
    public void AddTopic_NewTopic_AddsTopicToList()
    {
        // Arrange
        var resource = new Resource { Title = "Test Resource" };
        var topic = "Software Architecture";

        // Act
        resource.AddTopic(topic);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resource.Topics, Contains.Item(topic));
            Assert.That(resource.Topics.Count, Is.EqualTo(1));
            Assert.That(resource.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void AddTopic_DuplicateTopic_DoesNotAddDuplicate()
    {
        // Arrange
        var resource = new Resource { Title = "Test Resource" };
        var topic = "Design Patterns";

        // Act
        resource.AddTopic(topic);
        resource.AddTopic(topic);

        // Assert
        Assert.That(resource.Topics.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddTopic_DuplicateTopicDifferentCase_DoesNotAddDuplicate()
    {
        // Arrange
        var resource = new Resource { Title = "Test Resource" };

        // Act
        resource.AddTopic("Software Engineering");
        resource.AddTopic("software engineering");

        // Assert
        Assert.That(resource.Topics.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddTopic_MultipleTopics_AddsAllUniqueTopics()
    {
        // Arrange
        var resource = new Resource { Title = "Test Resource" };

        // Act
        resource.AddTopic("Architecture");
        resource.AddTopic("Design");
        resource.AddTopic("Testing");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resource.Topics.Count, Is.EqualTo(3));
            Assert.That(resource.Topics, Contains.Item("Architecture"));
            Assert.That(resource.Topics, Contains.Item("Design"));
            Assert.That(resource.Topics, Contains.Item("Testing"));
        });
    }

    [Test]
    public void Resource_WithPublicationDate_SetsCorrectly()
    {
        // Arrange
        var publicationDate = new DateTime(2020, 1, 15);

        // Act
        var resource = new Resource
        {
            Title = "Test Book",
            ResourceType = ResourceType.Book,
            PublicationDate = publicationDate
        };

        // Assert
        Assert.That(resource.PublicationDate, Is.EqualTo(publicationDate));
    }

    [Test]
    public void Resource_WithUrl_SetsCorrectly()
    {
        // Arrange
        var url = "https://example.com/article";

        // Act
        var resource = new Resource
        {
            Title = "Test Article",
            ResourceType = ResourceType.Article,
            Url = url
        };

        // Assert
        Assert.That(resource.Url, Is.EqualTo(url));
    }

    [Test]
    public void Resource_WithNotes_SetsCorrectly()
    {
        // Arrange
        var notes = "Highly recommended by colleague";

        // Act
        var resource = new Resource
        {
            Title = "Test Resource",
            Notes = notes
        };

        // Assert
        Assert.That(resource.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Resource_WithAllOptionalProperties_SetsAllProperties()
    {
        // Arrange & Act
        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Comprehensive Resource",
            ResourceType = ResourceType.ResearchPaper,
            Author = "Dr. Jane Smith",
            Publisher = "Academic Press",
            PublicationDate = new DateTime(2023, 5, 10),
            Url = "https://example.com/paper",
            Isbn = "978-1234567890",
            TotalPages = 250,
            Notes = "Important research"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resource.Title, Is.EqualTo("Comprehensive Resource"));
            Assert.That(resource.ResourceType, Is.EqualTo(ResourceType.ResearchPaper));
            Assert.That(resource.Author, Is.EqualTo("Dr. Jane Smith"));
            Assert.That(resource.Publisher, Is.EqualTo("Academic Press"));
            Assert.That(resource.PublicationDate, Is.EqualTo(new DateTime(2023, 5, 10)));
            Assert.That(resource.Url, Is.EqualTo("https://example.com/paper"));
            Assert.That(resource.Isbn, Is.EqualTo("978-1234567890"));
            Assert.That(resource.TotalPages, Is.EqualTo(250));
            Assert.That(resource.Notes, Is.EqualTo("Important research"));
        });
    }
}
