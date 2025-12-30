// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Sessions;

public class DeleteSessionCommand : IRequest<bool>
{
    public Guid SessionId { get; set; }
}

public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand, bool>
{
    private readonly IPokerGameTrackerContext _context;

    public DeleteSessionCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.Sessions
            .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken);

        if (session == null)
        {
            return false;
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
