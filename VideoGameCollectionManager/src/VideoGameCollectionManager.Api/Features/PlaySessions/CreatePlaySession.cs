// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.PlaySessions;

public class CreatePlaySessionCommand : IRequest<PlaySessionDto>
{
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Notes { get; set; }
}

public class CreatePlaySessionCommandHandler : IRequestHandler<CreatePlaySessionCommand, PlaySessionDto>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public CreatePlaySessionCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<PlaySessionDto> Handle(CreatePlaySessionCommand request, CancellationToken cancellationToken)
    {
        var playSession = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = request.UserId,
            GameId = request.GameId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            DurationMinutes = request.DurationMinutes,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.PlaySessions.Add(playSession);
        await _context.SaveChangesAsync(cancellationToken);

        return playSession.ToDto();
    }
}
