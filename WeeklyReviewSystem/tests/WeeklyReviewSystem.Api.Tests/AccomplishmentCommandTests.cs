// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WeeklyReviewSystem.Api.Features.Accomplishments;
using WeeklyReviewSystem.Core;
using WeeklyReviewSystem.Infrastructure;

namespace WeeklyReviewSystem.Api.Tests;

[TestFixture]
public class AccomplishmentCommandTests
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
    public async Task CreateAccomplishmentCommand_CreatesAccomplishment()
    {
        // Arrange
        var handler = new CreateAccomplishmentCommandHandler(_contextInterface);
        var command = new CreateAccomplishmentCommand
        {
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Completed project",
            Description = "Finished the big project ahead of schedule",
            Category = "Work",
            ImpactLevel = 9
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.Category, Is.EqualTo(command.Category));
        Assert.That(result.ImpactLevel, Is.EqualTo(command.ImpactLevel));

        var dbAccomplishment = await _context.Accomplishments.FindAsync(result.AccomplishmentId);
        Assert.That(dbAccomplishment, Is.Not.Null);
    }

    [Test]
    public async Task UpdateAccomplishmentCommand_UpdatesExistingAccomplishment()
    {
        // Arrange
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Original Title",
            ImpactLevel = 5
        };
        _context.Accomplishments.Add(accomplishment);
        await _context.SaveChangesAsync();

        var handler = new UpdateAccomplishmentCommandHandler(_contextInterface);
        var command = new UpdateAccomplishmentCommand
        {
            AccomplishmentId = accomplishment.AccomplishmentId,
            WeeklyReviewId = accomplishment.WeeklyReviewId,
            Title = "Updated Title",
            Description = "New description",
            ImpactLevel = 10
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Title, Is.EqualTo("Updated Title"));
        Assert.That(result.Description, Is.EqualTo("New description"));
        Assert.That(result.ImpactLevel, Is.EqualTo(10));
    }

    [Test]
    public async Task UpdateAccomplishmentCommand_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var handler = new UpdateAccomplishmentCommandHandler(_contextInterface);
        var command = new UpdateAccomplishmentCommand
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Title"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task DeleteAccomplishmentCommand_DeletesExistingAccomplishment()
    {
        // Arrange
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "To be deleted"
        };
        _context.Accomplishments.Add(accomplishment);
        await _context.SaveChangesAsync();

        var handler = new DeleteAccomplishmentCommandHandler(_contextInterface);
        var command = new DeleteAccomplishmentCommand { AccomplishmentId = accomplishment.AccomplishmentId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        var dbAccomplishment = await _context.Accomplishments.FindAsync(accomplishment.AccomplishmentId);
        Assert.That(dbAccomplishment, Is.Null);
    }

    [Test]
    public async Task DeleteAccomplishmentCommand_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        var handler = new DeleteAccomplishmentCommandHandler(_contextInterface);
        var command = new DeleteAccomplishmentCommand { AccomplishmentId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
    }
}
