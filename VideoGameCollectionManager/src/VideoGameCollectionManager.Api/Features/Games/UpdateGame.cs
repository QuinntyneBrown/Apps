// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Games;

public class UpdateGameCommand : IRequest<GameDto?>
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
}

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, GameDto?>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public UpdateGameCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<GameDto?> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.GameId == request.GameId, cancellationToken);

        if (game == null)
        {
            return null;
        }

        game.UserId = request.UserId;
        game.Title = request.Title;
        game.Platform = request.Platform;
        game.Genre = request.Genre;
        game.Status = request.Status;
        game.Publisher = request.Publisher;
        game.Developer = request.Developer;
        game.ReleaseDate = request.ReleaseDate;
        game.PurchaseDate = request.PurchaseDate;
        game.PurchasePrice = request.PurchasePrice;
        game.Rating = request.Rating;
        game.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return game.ToDto();
    }
}
