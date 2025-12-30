// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core.Tests;

public class ResourceTypeTests
{
    [Test]
    public void ResourceType_Book_CanBeAssigned()
    {
        // Arrange & Act
        var resourceType = ResourceType.Book;

        // Assert
        Assert.That(resourceType, Is.EqualTo(ResourceType.Book));
        Assert.That((int)resourceType, Is.EqualTo(0));
    }

    [Test]
    public void ResourceType_Article_CanBeAssigned()
    {
        // Arrange & Act
        var resourceType = ResourceType.Article;

        // Assert
        Assert.That(resourceType, Is.EqualTo(ResourceType.Article));
        Assert.That((int)resourceType, Is.EqualTo(1));
    }

    [Test]
    public void ResourceType_ResearchPaper_CanBeAssigned()
    {
        // Arrange & Act
        var resourceType = ResourceType.ResearchPaper;

        // Assert
        Assert.That(resourceType, Is.EqualTo(ResourceType.ResearchPaper));
        Assert.That((int)resourceType, Is.EqualTo(2));
    }

    [Test]
    public void ResourceType_BlogPost_CanBeAssigned()
    {
        // Arrange & Act
        var resourceType = ResourceType.BlogPost;

        // Assert
        Assert.That(resourceType, Is.EqualTo(ResourceType.BlogPost));
        Assert.That((int)resourceType, Is.EqualTo(3));
    }

    [Test]
    public void ResourceType_Video_CanBeAssigned()
    {
        // Arrange & Act
        var resourceType = ResourceType.Video;

        // Assert
        Assert.That(resourceType, Is.EqualTo(ResourceType.Video));
        Assert.That((int)resourceType, Is.EqualTo(4));
    }

    [Test]
    public void ResourceType_Podcast_CanBeAssigned()
    {
        // Arrange & Act
        var resourceType = ResourceType.Podcast;

        // Assert
        Assert.That(resourceType, Is.EqualTo(ResourceType.Podcast));
        Assert.That((int)resourceType, Is.EqualTo(5));
    }

    [Test]
    public void ResourceType_Whitepaper_CanBeAssigned()
    {
        // Arrange & Act
        var resourceType = ResourceType.Whitepaper;

        // Assert
        Assert.That(resourceType, Is.EqualTo(ResourceType.Whitepaper));
        Assert.That((int)resourceType, Is.EqualTo(6));
    }

    [Test]
    public void ResourceType_Other_CanBeAssigned()
    {
        // Arrange & Act
        var resourceType = ResourceType.Other;

        // Assert
        Assert.That(resourceType, Is.EqualTo(ResourceType.Other));
        Assert.That((int)resourceType, Is.EqualTo(7));
    }

    [Test]
    public void ResourceType_AllValues_AreUnique()
    {
        // Arrange
        var values = Enum.GetValues<ResourceType>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }

    [Test]
    public void ResourceType_HasExpectedNumberOfValues()
    {
        // Arrange & Act
        var values = Enum.GetValues<ResourceType>();

        // Assert
        Assert.That(values.Length, Is.EqualTo(8));
    }
}
