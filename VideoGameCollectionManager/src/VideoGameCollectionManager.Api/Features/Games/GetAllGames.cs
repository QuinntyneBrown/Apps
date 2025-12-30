// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Games;

public class GetAllGamesQuery : IRequest<List<GameDto>>
{
}

public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, List<GameDto>>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public GetAllGamesQueryHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<List<GameDto>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Games
            .Select(g => g.ToDto())
            .ToListAsync(cancellationToken);
    }
}
