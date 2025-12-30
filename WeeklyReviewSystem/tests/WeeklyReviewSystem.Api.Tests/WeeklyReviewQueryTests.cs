// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WeeklyReviewSystem.Api.Features.WeeklyReviews;
using WeeklyReviewSystem.Core;
using WeeklyReviewSystem.Infrastructure;

namespace WeeklyReviewSystem.Api.Tests;

[TestFixture]
public class WeeklyReviewQueryTests
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
    public async Task GetAllWeeklyReviewsQuery_ReturnsAllReviews()
    {
        // Arrange
        var review1 = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 1),
            WeekEndDate = new DateTime(2024, 1, 7)
        };
        var review2 = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 8),
            WeekEndDate = new DateTime(2024, 1, 14)
        };
        _context.Reviews.AddRange(review1, review2);
        await _context.SaveChangesAsync();

        var handler = new GetAllWeeklyReviewsQueryHandler(_contextInterface);
        var query = new GetAllWeeklyReviewsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(r => r.WeeklyReviewId == review1.WeeklyReviewId), Is.True);
        Assert.That(result.Any(r => r.WeeklyReviewId == review2.WeeklyReviewId), Is.True);
    }

    [Test]
    public async Task GetAllWeeklyReviewsQuery_WithNoReviews_ReturnsEmptyList()
    {
        // Arrange
        var handler = new GetAllWeeklyReviewsQueryHandler(_contextInterface);
        var query = new GetAllWeeklyReviewsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetWeeklyReviewByIdQuery_WithValidId_ReturnsReview()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 1),
            WeekEndDate = new DateTime(2024, 1, 7),
            OverallRating = 8
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var handler = new GetWeeklyReviewByIdQueryHandler(_contextInterface);
        var query = new GetWeeklyReviewByIdQuery { WeeklyReviewId = review.WeeklyReviewId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.WeeklyReviewId, Is.EqualTo(review.WeeklyReviewId));
        Assert.That(result.UserId, Is.EqualTo(review.UserId));
        Assert.That(result.OverallRating, Is.EqualTo(8));
    }

    [Test]
    public async Task GetWeeklyReviewByIdQuery_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var handler = new GetWeeklyReviewByIdQueryHandler(_contextInterface);
        var query = new GetWeeklyReviewByIdQuery { WeeklyReviewId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}
