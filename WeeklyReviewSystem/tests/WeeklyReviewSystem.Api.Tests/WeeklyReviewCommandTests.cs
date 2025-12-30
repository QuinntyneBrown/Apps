// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WeeklyReviewSystem.Api.Features.WeeklyReviews;
using WeeklyReviewSystem.Core;
using WeeklyReviewSystem.Infrastructure;

namespace WeeklyReviewSystem.Api.Tests;

[TestFixture]
public class WeeklyReviewCommandTests
{
    private WeeklyReviewSystemContext _context = null!;
    private IWeeklyReviewSystemContext _contextInterface = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WeeklyReviewSystemContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WeeklyReviewSystemContext(options);
        _contextInterface = _context;
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateWeeklyReviewCommand_CreatesReview()
    {
        // Arrange
        var handler = new CreateWeeklyReviewCommandHandler(_contextInterface);
        var command = new CreateWeeklyReviewCommand
        {
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 1),
            WeekEndDate = new DateTime(2024, 1, 7),
            OverallRating = 8,
            Reflections = "Good week"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.WeekStartDate, Is.EqualTo(command.WeekStartDate));
        Assert.That(result.WeekEndDate, Is.EqualTo(command.WeekEndDate));
        Assert.That(result.OverallRating, Is.EqualTo(command.OverallRating));
        Assert.That(result.Reflections, Is.EqualTo(command.Reflections));
        Assert.That(result.IsCompleted, Is.False);

        var dbReview = await _context.Reviews.FindAsync(result.WeeklyReviewId);
        Assert.That(dbReview, Is.Not.Null);
    }

    [Test]
    public async Task UpdateWeeklyReviewCommand_UpdatesExistingReview()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 1),
            WeekEndDate = new DateTime(2024, 1, 7)
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var handler = new UpdateWeeklyReviewCommandHandler(_contextInterface);
        var command = new UpdateWeeklyReviewCommand
        {
            WeeklyReviewId = review.WeeklyReviewId,
            UserId = review.UserId,
            WeekStartDate = review.WeekStartDate,
            WeekEndDate = review.WeekEndDate,
            OverallRating = 9,
            Reflections = "Updated reflections",
            IsCompleted = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.OverallRating, Is.EqualTo(9));
        Assert.That(result.Reflections, Is.EqualTo("Updated reflections"));
        Assert.That(result.IsCompleted, Is.True);
    }

    [Test]
    public async Task UpdateWeeklyReviewCommand_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var handler = new UpdateWeeklyReviewCommandHandler(_contextInterface);
        var command = new UpdateWeeklyReviewCommand
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = DateTime.UtcNow,
            WeekEndDate = DateTime.UtcNow.AddDays(7)
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task DeleteWeeklyReviewCommand_DeletesExistingReview()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 1),
            WeekEndDate = new DateTime(2024, 1, 7)
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var handler = new DeleteWeeklyReviewCommandHandler(_contextInterface);
        var command = new DeleteWeeklyReviewCommand { WeeklyReviewId = review.WeeklyReviewId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        var dbReview = await _context.Reviews.FindAsync(review.WeeklyReviewId);
        Assert.That(dbReview, Is.Null);
    }

    [Test]
    public async Task DeleteWeeklyReviewCommand_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        var handler = new DeleteWeeklyReviewCommandHandler(_contextInterface);
        var command = new DeleteWeeklyReviewCommand { WeeklyReviewId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
    }
}
