// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.PlaySessions;

public class GetPlaySessionByIdQuery : IRequest<PlaySessionDto?>
{
    public Guid PlaySessionId { get; set; }
}

public class GetPlaySessionByIdQueryHandler : IRequestHandler<GetPlaySessionByIdQuery, PlaySessionDto?>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public GetPlaySessionByIdQueryHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<PlaySessionDto?> Handle(GetPlaySessionByIdQuery request, CancellationToken cancellationToken)
    {
        var playSession = await _context.PlaySessions
            .FirstOrDefaultAsync(p => p.PlaySessionId == request.PlaySessionId, cancellationToken);

        return playSession?.ToDto();
    }
}
