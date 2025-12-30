// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Wishlists;

public class WishlistDto
{
    public Guid WishlistId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Platform? Platform { get; set; }
    public Genre? Genre { get; set; }
    public int Priority { get; set; }
    public string? Notes { get; set; }
    public bool IsAcquired { get; set; }
    public DateTime CreatedAt { get; set; }
}

public static class WishlistDtoExtensions
{
    public static WishlistDto ToDto(this Wishlist wishlist)
    {
        return new WishlistDto
        {
            WishlistId = wishlist.WishlistId,
            UserId = wishlist.UserId,
            Title = wishlist.Title,
            Platform = wishlist.Platform,
            Genre = wishlist.Genre,
            Priority = wishlist.Priority,
            Notes = wishlist.Notes,
            IsAcquired = wishlist.IsAcquired,
            CreatedAt = wishlist.CreatedAt
        };
    }
}
