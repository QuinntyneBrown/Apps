// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Games;

public class DeleteGameCommand : IRequest<bool>
{
    public Guid GameId { get; set; }
}

public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, bool>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public DeleteGameCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.GameId == request.GameId, cancellationToken);

        if (game == null)
        {
            return false;
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
