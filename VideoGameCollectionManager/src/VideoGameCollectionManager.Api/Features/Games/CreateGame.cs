// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Games;

public class CreateGameCommand : IRequest<GameDto>
{
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

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameDto>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public CreateGameCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<GameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Platform = request.Platform,
            Genre = request.Genre,
            Status = request.Status,
            Publisher = request.Publisher,
            Developer = request.Developer,
            ReleaseDate = request.ReleaseDate,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            Rating = request.Rating,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync(cancellationToken);

        return game.ToDto();
    }
}
