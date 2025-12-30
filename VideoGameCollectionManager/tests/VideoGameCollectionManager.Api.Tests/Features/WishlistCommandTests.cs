// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Api.Features.Wishlists;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Tests.Features;

[TestFixture]
public class WishlistCommandTests
{
    private IVideoGameCollectionManagerContext _context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        if (_context is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    [Test]
    public async Task CreateWishlist_ValidCommand_CreatesWishlist()
    {
        // Arrange
        var handler = new CreateWishlistCommandHandler(_context);
        var command = new CreateWishlistCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Test Wishlist Item",
            Platform = Platform.PlayStation5,
            Genre = Genre.Action,
            Priority = 1,
            Notes = "Want to play this",
            IsAcquired = false
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Platform, Is.EqualTo(command.Platform));
        Assert.That(result.Priority, Is.EqualTo(command.Priority));
        Assert.That(result.IsAcquired, Is.EqualTo(command.IsAcquired));

        var itemInDb = await _context.Wishlists.FindAsync(result.WishlistId);
        Assert.That(itemInDb, Is.Not.Null);
    }

    [Test]
    public async Task UpdateWishlist_ExistingItem_UpdatesSuccessfully()
    {
        // Arrange
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Original Title",
            Priority = 3,
            IsAcquired = false
        };

        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();

        var handler = new UpdateWishlistCommandHandler(_context);
        var command = new UpdateWishlistCommand
        {
            WishlistId = wishlist.WishlistId,
            UserId = wishlist.UserId,
            Title = "Updated Title",
            Priority = 1,
            IsAcquired = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Title, Is.EqualTo("Updated Title"));
        Assert.That(result.Priority, Is.EqualTo(1));
        Assert.That(result.IsAcquired, Is.True);
    }

    [Test]
    public async Task DeleteWishlist_ExistingItem_DeletesSuccessfully()
    {
        // Arrange
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Item",
            Priority = 2
        };

        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();

        var handler = new DeleteWishlistCommandHandler(_context);
        var command = new DeleteWishlistCommand { WishlistId = wishlist.WishlistId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);

        var itemInDb = await _context.Wishlists.FindAsync(wishlist.WishlistId);
        Assert.That(itemInDb, Is.Null);
    }

    [Test]
    public async Task GetAllWishlists_ReturnsAllItems()
    {
        // Arrange
        var wishlist1 = new Wishlist { WishlistId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Item 1", Priority = 1 };
        var wishlist2 = new Wishlist { WishlistId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Item 2", Priority = 2 };

        _context.Wishlists.Add(wishlist1);
        _context.Wishlists.Add(wishlist2);
        await _context.SaveChangesAsync();

        var handler = new GetAllWishlistsQueryHandler(_context);
        var query = new GetAllWishlistsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
    }
}
