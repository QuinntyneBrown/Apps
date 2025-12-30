// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Games;

public class GameDto
{
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Platform Platform { get; set; }
    public Genre Genre { get; set; }
    public CompletionStatus Status { get; set; }
    public string? Publisher { get; set; }
    public string? Developer { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public int? Rating { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public static class GameDtoExtensions
{
    public static GameDto ToDto(this Game game)
    {
        return new GameDto
        {
            GameId = game.GameId,
            UserId = game.UserId,
            Title = game.Title,
            Platform = game.Platform,
            Genre = game.Genre,
            Status = game.Status,
            Publisher = game.Publisher,
            Developer = game.Developer,
            ReleaseDate = game.ReleaseDate,
            PurchaseDate = game.PurchaseDate,
            PurchasePrice = game.PurchasePrice,
            Rating = game.Rating,
            Notes = game.Notes,
            CreatedAt = game.CreatedAt
        };
    }
}
