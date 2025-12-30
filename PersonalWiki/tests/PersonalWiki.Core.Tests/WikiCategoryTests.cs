// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core.Tests;

public class WikiCategoryTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesWikiCategory()
    {
        // Arrange & Act
        var category = new WikiCategory();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.WikiCategoryId, Is.EqualTo(Guid.Empty));
            Assert.That(category.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(category.Name, Is.EqualTo(string.Empty));
            Assert.That(category.Description, Is.Null);
            Assert.That(category.ParentCategoryId, Is.Null);
            Assert.That(category.Icon, Is.Null);
            Assert.That(category.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(category.ParentCategory, Is.Null);
            Assert.That(category.ChildCategories, Is.Not.Null);
            Assert.That(category.ChildCategories, Is.Empty);
            Assert.That(category.Pages, Is.Not.Null);
            Assert.That(category.Pages, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var parentCategoryId = Guid.NewGuid();

        // Act
        var category = new WikiCategory
        {
            WikiCategoryId = categoryId,
            UserId = userId,
            Name = "Test Category",
            Description = "Test Description",
            ParentCategoryId = parentCategoryId,
            Icon = "📚"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.WikiCategoryId, Is.EqualTo(categoryId));
            Assert.That(category.UserId, Is.EqualTo(userId));
            Assert.That(category.Name, Is.EqualTo("Test Category"));
            Assert.That(category.Description, Is.EqualTo("Test Description"));
            Assert.That(category.ParentCategoryId, Is.EqualTo(parentCategoryId));
            Assert.That(category.Icon, Is.EqualTo("📚"));
        });
    }

    [Test]
    public void GetPageCount_WhenNoPages_ReturnsZero()
    {
        // Arrange
        var category = new WikiCategory();

        // Act
        var count = category.GetPageCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetPageCount_WhenPagesExist_ReturnsCorrectCount()
    {
        // Arrange
        var category = new WikiCategory
        {
            Pages = new List<WikiPage>
            {
                new WikiPage { WikiPageId = Guid.NewGuid(), Title = "Page 1" },
                new WikiPage { WikiPageId = Guid.NewGuid(), Title = "Page 2" },
                new WikiPage { WikiPageId = Guid.NewGuid(), Title = "Page 3" }
            }
        };

        // Act
        var count = category.GetPageCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void Pages_Collection_CanBeModified()
    {
        // Arrange
        var category = new WikiCategory();
        var page = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            Title = "Test Page"
        };

        // Act
        category.Pages.Add(page);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.Pages.Count, Is.EqualTo(1));
            Assert.That(category.Pages.First(), Is.EqualTo(page));
        });
    }

    [Test]
    public void ChildCategories_Collection_CanBeModified()
    {
        // Arrange
        var parentCategory = new WikiCategory();
        var childCategory = new WikiCategory
        {
            WikiCategoryId = Guid.NewGuid(),
            Name = "Child Category"
        };

        // Act
        parentCategory.ChildCategories.Add(childCategory);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(parentCategory.ChildCategories.Count, Is.EqualTo(1));
            Assert.That(parentCategory.ChildCategories.First(), Is.EqualTo(childCategory));
        });
    }

    [Test]
    public void ParentCategory_NavigationProperty_CanBeSet()
    {
        // Arrange
        var childCategory = new WikiCategory();
        var parentCategory = new WikiCategory
        {
            WikiCategoryId = Guid.NewGuid(),
            Name = "Parent Category"
        };

        // Act
        childCategory.ParentCategory = parentCategory;

        // Assert
        Assert.That(childCategory.ParentCategory, Is.EqualTo(parentCategory));
    }

    [Test]
    public void ParentCategoryId_CanBeNull()
    {
        // Arrange & Act
        var category = new WikiCategory
        {
            ParentCategoryId = null
        };

        // Assert
        Assert.That(category.ParentCategoryId, Is.Null);
    }

    [Test]
    public void Icon_CanBeSet()
    {
        // Arrange & Act
        var category = new WikiCategory
        {
            Icon = "🏷️"
        };

        // Assert
        Assert.That(category.Icon, Is.EqualTo("🏷️"));
    }
}
