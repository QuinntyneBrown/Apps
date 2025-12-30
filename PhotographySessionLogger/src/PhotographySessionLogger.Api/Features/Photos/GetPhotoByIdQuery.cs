using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Photos;

public record GetPhotoByIdQuery : IRequest<PhotoDto?>
{
    public Guid PhotoId { get; init; }
}

public class GetPhotoByIdQueryHandler : IRequestHandler<GetPhotoByIdQuery, PhotoDto?>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetPhotoByIdQueryHandler> _logger;

    public GetPhotoByIdQueryHandler(
        IPhotographySessionLoggerContext context,
        ILogger<GetPhotoByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PhotoDto?> Handle(GetPhotoByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting photo {PhotoId}", request.PhotoId);

        var photo = await _context.Photos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PhotoId == request.PhotoId, cancellationToken);

        return photo?.ToDto();
    }
}
