// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core.Tests;

public class PageStatusTests
{
    [Test]
    public void Draft_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PageStatus.Draft, Is.EqualTo(0));
    }

    [Test]
    public void Published_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PageStatus.Published, Is.EqualTo(1));
    }

    [Test]
    public void Review_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PageStatus.Review, Is.EqualTo(2));
    }

    [Test]
    public void Archived_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PageStatus.Archived, Is.EqualTo(3));
    }

    [Test]
    public void AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var draft = PageStatus.Draft;
        var published = PageStatus.Published;
        var review = PageStatus.Review;
        var archived = PageStatus.Archived;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(draft, Is.EqualTo(PageStatus.Draft));
            Assert.That(published, Is.EqualTo(PageStatus.Published));
            Assert.That(review, Is.EqualTo(PageStatus.Review));
            Assert.That(archived, Is.EqualTo(PageStatus.Archived));
        });
    }
}
