// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.PlaySessions;

public class DeletePlaySessionCommand : IRequest<bool>
{
    public Guid PlaySessionId { get; set; }
}

public class DeletePlaySessionCommandHandler : IRequestHandler<DeletePlaySessionCommand, bool>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public DeletePlaySessionCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePlaySessionCommand request, CancellationToken cancellationToken)
    {
        var playSession = await _context.PlaySessions
            .FirstOrDefaultAsync(p => p.PlaySessionId == request.PlaySessionId, cancellationToken);

        if (playSession == null)
        {
            return false;
        }

        _context.PlaySessions.Remove(playSession);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
