using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void WikiCategoryDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var category = new WikiCategory
        {
            WikiCategoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Technology",
            Description = "Tech-related pages",
            ParentCategoryId = Guid.NewGuid(),
            Icon = "💻",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = category.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.WikiCategoryId, Is.EqualTo(category.WikiCategoryId));
            Assert.That(dto.UserId, Is.EqualTo(category.UserId));
            Assert.That(dto.Name, Is.EqualTo(category.Name));
            Assert.That(dto.Description, Is.EqualTo(category.Description));
            Assert.That(dto.ParentCategoryId, Is.EqualTo(category.ParentCategoryId));
            Assert.That(dto.Icon, Is.EqualTo(category.Icon));
            Assert.That(dto.CreatedAt, Is.EqualTo(category.CreatedAt));
            Assert.That(dto.PageCount, Is.EqualTo(category.GetPageCount()));
        });
    }

    [Test]
    public void WikiPageDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var page = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid(),
            Title = "C# Best Practices",
            Slug = "csharp-best-practices",
            Content = "# C# Best Practices\n\nContent here...",
            Status = PageStatus.Published,
            Version = 1,
            IsFeatured = true,
            ViewCount = 42,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow.AddDays(-7),
        };

        // Act
        var dto = page.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.WikiPageId, Is.EqualTo(page.WikiPageId));
            Assert.That(dto.UserId, Is.EqualTo(page.UserId));
            Assert.That(dto.CategoryId, Is.EqualTo(page.CategoryId));
            Assert.That(dto.Title, Is.EqualTo(page.Title));
            Assert.That(dto.Slug, Is.EqualTo(page.Slug));
            Assert.That(dto.Content, Is.EqualTo(page.Content));
            Assert.That(dto.Status, Is.EqualTo(page.Status));
            Assert.That(dto.Version, Is.EqualTo(page.Version));
            Assert.That(dto.IsFeatured, Is.EqualTo(page.IsFeatured));
            Assert.That(dto.ViewCount, Is.EqualTo(page.ViewCount));
            Assert.That(dto.LastModifiedAt, Is.EqualTo(page.LastModifiedAt));
            Assert.That(dto.CreatedAt, Is.EqualTo(page.CreatedAt));
        });
    }

    [Test]
    public void PageRevisionDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var revision = new PageRevision
        {
            PageRevisionId = Guid.NewGuid(),
            WikiPageId = Guid.NewGuid(),
            Version = 2,
            Content = "Updated content",
            ChangeSummary = "Fixed typos",
            RevisedBy = "John Doe",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = revision.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PageRevisionId, Is.EqualTo(revision.PageRevisionId));
            Assert.That(dto.WikiPageId, Is.EqualTo(revision.WikiPageId));
            Assert.That(dto.Version, Is.EqualTo(revision.Version));
            Assert.That(dto.Content, Is.EqualTo(revision.Content));
            Assert.That(dto.ChangeSummary, Is.EqualTo(revision.ChangeSummary));
            Assert.That(dto.RevisedBy, Is.EqualTo(revision.RevisedBy));
            Assert.That(dto.CreatedAt, Is.EqualTo(revision.CreatedAt));
        });
    }

    [Test]
    public void PageLinkDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var link = new PageLink
        {
            PageLinkId = Guid.NewGuid(),
            SourcePageId = Guid.NewGuid(),
            TargetPageId = Guid.NewGuid(),
            AnchorText = "Related Page",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = link.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PageLinkId, Is.EqualTo(link.PageLinkId));
            Assert.That(dto.SourcePageId, Is.EqualTo(link.SourcePageId));
            Assert.That(dto.TargetPageId, Is.EqualTo(link.TargetPageId));
            Assert.That(dto.AnchorText, Is.EqualTo(link.AnchorText));
            Assert.That(dto.CreatedAt, Is.EqualTo(link.CreatedAt));
        });
    }
}

[TestFixture]
public class CommandTests
{
    private Mock<IPersonalWikiContext> _mockContext = null!;
    private Mock<ILogger<CreateWikiCategoryCommandHandler>> _mockLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IPersonalWikiContext>();
        _mockLogger = new Mock<ILogger<CreateWikiCategoryCommandHandler>>();
    }

    [Test]
    public async Task CreateWikiCategoryCommand_CreatesCategory_Successfully()
    {
        // Arrange
        var categories = new List<WikiCategory>();
        var mockSet = TestHelpers.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Categories).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateWikiCategoryCommandHandler(_mockContext.Object, _mockLogger.Object);
        var command = new CreateWikiCategoryCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Test Category",
            Description = "Test Description",
            Icon = "📁",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Description, Is.EqualTo(command.Description));
            Assert.That(result.Icon, Is.EqualTo(command.Icon));
            Assert.That(result.UserId, Is.EqualTo(command.UserId));
        });

        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetWikiCategoriesQuery_ReturnsFilteredCategories()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var categories = new List<WikiCategory>
        {
            new WikiCategory
            {
                WikiCategoryId = Guid.NewGuid(),
                UserId = userId,
                Name = "Category 1",
                CreatedAt = DateTime.UtcNow,
            },
            new WikiCategory
            {
                WikiCategoryId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Category 2",
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockSet = TestHelpers.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

        var logger = new Mock<ILogger<GetWikiCategoriesQueryHandler>>();
        var handler = new GetWikiCategoriesQueryHandler(_mockContext.Object, logger.Object);
        var query = new GetWikiCategoriesQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo("Category 1"));
    }

    [Test]
    public async Task UpdateWikiPageCommand_UpdatesContent_AndIncrementsVersion()
    {
        // Arrange
        var pageId = Guid.NewGuid();
        var originalContent = "Original content";
        var newContent = "Updated content";

        var pages = new List<WikiPage>
        {
            new WikiPage
            {
                WikiPageId = pageId,
                UserId = Guid.NewGuid(),
                Title = "Test Page",
                Slug = "test-page",
                Content = originalContent,
                Status = PageStatus.Draft,
                Version = 1,
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockSet = TestHelpers.CreateMockDbSet(pages);
        _mockContext.Setup(c => c.Pages).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var logger = new Mock<ILogger<UpdateWikiPageCommandHandler>>();
        var handler = new UpdateWikiPageCommandHandler(_mockContext.Object, logger.Object);
        var command = new UpdateWikiPageCommand
        {
            WikiPageId = pageId,
            Title = "Updated Title",
            Slug = "updated-slug",
            Content = newContent,
            Status = PageStatus.Published,
            IsFeatured = true,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Content, Is.EqualTo(newContent));
            Assert.That(result.Version, Is.EqualTo(2)); // Version should increment
            Assert.That(result.Status, Is.EqualTo(PageStatus.Published));
            Assert.That(result.IsFeatured, Is.True);
        });

        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteWikiCategoryCommand_DeletesCategory_WhenExists()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var categories = new List<WikiCategory>
        {
            new WikiCategory
            {
                WikiCategoryId = categoryId,
                UserId = Guid.NewGuid(),
                Name = "Category to Delete",
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockSet = TestHelpers.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Categories).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var logger = new Mock<ILogger<DeleteWikiCategoryCommandHandler>>();
        var handler = new DeleteWikiCategoryCommandHandler(_mockContext.Object, logger.Object);
        var command = new DeleteWikiCategoryCommand { WikiCategoryId = categoryId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteWikiCategoryCommand_ReturnsFalse_WhenCategoryNotFound()
    {
        // Arrange
        var categories = new List<WikiCategory>();
        var mockSet = TestHelpers.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

        var logger = new Mock<ILogger<DeleteWikiCategoryCommandHandler>>();
        var handler = new DeleteWikiCategoryCommandHandler(_mockContext.Object, logger.Object);
        var command = new DeleteWikiCategoryCommand { WikiCategoryId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
