// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Games;

public class GetGameByIdQuery : IRequest<GameDto?>
{
    public Guid GameId { get; set; }
}

public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameDto?>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public GetGameByIdQueryHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<GameDto?> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.GameId == request.GameId, cancellationToken);

        return game?.ToDto();
    }
}
