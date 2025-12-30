// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.PlaySessions;

public class GetAllPlaySessionsQuery : IRequest<List<PlaySessionDto>>
{
}

public class GetAllPlaySessionsQueryHandler : IRequestHandler<GetAllPlaySessionsQuery, List<PlaySessionDto>>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public GetAllPlaySessionsQueryHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<List<PlaySessionDto>> Handle(GetAllPlaySessionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.PlaySessions
            .Select(p => p.ToDto())
            .ToListAsync(cancellationToken);
    }
}
