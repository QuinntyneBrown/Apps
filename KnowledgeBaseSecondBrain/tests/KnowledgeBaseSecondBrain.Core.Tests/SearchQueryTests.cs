// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core.Tests;

public class SearchQueryTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesSearchQuery()
    {
        // Arrange
        var searchQueryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var queryText = "test query";
        var name = "My Search";

        // Act
        var query = new SearchQuery
        {
            SearchQueryId = searchQueryId,
            UserId = userId,
            QueryText = queryText,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(query.SearchQueryId, Is.EqualTo(searchQueryId));
            Assert.That(query.UserId, Is.EqualTo(userId));
            Assert.That(query.QueryText, Is.EqualTo(queryText));
            Assert.That(query.Name, Is.EqualTo(name));
            Assert.That(query.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Execute_WhenCalled_IncrementsExecutionCount()
    {
        // Arrange
        var query = new SearchQuery { ExecutionCount = 0 };

        // Act
        query.Execute();

        // Assert
        Assert.That(query.ExecutionCount, Is.EqualTo(1));
    }

    [Test]
    public void Execute_WhenCalled_UpdatesLastExecutedAt()
    {
        // Arrange
        var query = new SearchQuery { LastExecutedAt = null };
        var beforeExecution = DateTime.UtcNow;

        // Act
        query.Execute();

        var afterExecution = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(query.LastExecutedAt, Is.Not.Null);
            Assert.That(query.LastExecutedAt, Is.GreaterThanOrEqualTo(beforeExecution));
            Assert.That(query.LastExecutedAt, Is.LessThanOrEqualTo(afterExecution));
        });
    }

    [Test]
    public void Execute_CalledMultipleTimes_IncrementsCountCorrectly()
    {
        // Arrange
        var query = new SearchQuery { ExecutionCount = 0 };

        // Act
        query.Execute();
        query.Execute();
        query.Execute();

        // Assert
        Assert.That(query.ExecutionCount, Is.EqualTo(3));
    }

    [Test]
    public void IsSaved_DefaultsToFalse()
    {
        // Arrange & Act
        var query = new SearchQuery();

        // Assert
        Assert.That(query.IsSaved, Is.False);
    }

    [Test]
    public void IsSaved_CanBeSetToTrue()
    {
        // Arrange
        var query = new SearchQuery();

        // Act
        query.IsSaved = true;

        // Assert
        Assert.That(query.IsSaved, Is.True);
    }

    [Test]
    public void ExecutionCount_DefaultsToZero()
    {
        // Arrange & Act
        var query = new SearchQuery();

        // Assert
        Assert.That(query.ExecutionCount, Is.EqualTo(0));
    }

    [Test]
    public void LastExecutedAt_DefaultsToNull()
    {
        // Arrange & Act
        var query = new SearchQuery();

        // Assert
        Assert.That(query.LastExecutedAt, Is.Null);
    }

    [Test]
    public void QueryText_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var query = new SearchQuery();
        var expectedText = "find important notes";

        // Act
        query.QueryText = expectedText;

        // Assert
        Assert.That(query.QueryText, Is.EqualTo(expectedText));
    }

    [Test]
    public void Name_CanBeNull()
    {
        // Arrange
        var query = new SearchQuery();

        // Act
        query.Name = null;

        // Assert
        Assert.That(query.Name, Is.Null);
    }
}
