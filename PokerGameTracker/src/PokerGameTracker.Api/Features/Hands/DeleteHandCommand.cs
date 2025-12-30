// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Hands;

public class DeleteHandCommand : IRequest<bool>
{
    public Guid HandId { get; set; }
}

public class DeleteHandCommandHandler : IRequestHandler<DeleteHandCommand, bool>
{
    private readonly IPokerGameTrackerContext _context;

    public DeleteHandCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteHandCommand request, CancellationToken cancellationToken)
    {
        var hand = await _context.Hands
            .FirstOrDefaultAsync(h => h.HandId == request.HandId, cancellationToken);

        if (hand == null)
        {
            return false;
        }

        _context.Hands.Remove(hand);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
