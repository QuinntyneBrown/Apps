using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Photos;

public record GetPhotosQuery : IRequest<IEnumerable<PhotoDto>>
{
    public Guid? UserId { get; init; }
    public Guid? SessionId { get; init; }
    public int? MinRating { get; init; }
}

public class GetPhotosQueryHandler : IRequestHandler<GetPhotosQuery, IEnumerable<PhotoDto>>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetPhotosQueryHandler> _logger;

    public GetPhotosQueryHandler(
        IPhotographySessionLoggerContext context,
        ILogger<GetPhotosQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PhotoDto>> Handle(GetPhotosQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting photos for user {UserId}, session {SessionId}", request.UserId, request.SessionId);

        var query = _context.Photos.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(p => p.UserId == request.UserId.Value);
        }

        if (request.SessionId.HasValue)
        {
            query = query.Where(p => p.SessionId == request.SessionId.Value);
        }

        if (request.MinRating.HasValue)
        {
            query = query.Where(p => p.Rating.HasValue && p.Rating.Value >= request.MinRating.Value);
        }

        var photos = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return photos.Select(p => p.ToDto());
    }
}
