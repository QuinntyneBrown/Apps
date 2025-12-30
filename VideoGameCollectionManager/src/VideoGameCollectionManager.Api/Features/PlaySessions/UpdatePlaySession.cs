// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.PlaySessions;

public class UpdatePlaySessionCommand : IRequest<PlaySessionDto?>
{
    public Guid PlaySessionId { get; set; }
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Notes { get; set; }
}

public class UpdatePlaySessionCommandHandler : IRequestHandler<UpdatePlaySessionCommand, PlaySessionDto?>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public UpdatePlaySessionCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<PlaySessionDto?> Handle(UpdatePlaySessionCommand request, CancellationToken cancellationToken)
    {
        var playSession = await _context.PlaySessions
            .FirstOrDefaultAsync(p => p.PlaySessionId == request.PlaySessionId, cancellationToken);

        if (playSession == null)
        {
            return null;
        }

        playSession.UserId = request.UserId;
        playSession.GameId = request.GameId;
        playSession.StartTime = request.StartTime;
        playSession.EndTime = request.EndTime;
        playSession.DurationMinutes = request.DurationMinutes;
        playSession.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return playSession.ToDto();
    }
}
