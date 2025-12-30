using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Photos;

public record DeletePhotoCommand : IRequest<bool>
{
    public Guid PhotoId { get; init; }
}

public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, bool>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<DeletePhotoCommandHandler> _logger;

    public DeletePhotoCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<DeletePhotoCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting photo {PhotoId}", request.PhotoId);

        var photo = await _context.Photos
            .FirstOrDefaultAsync(p => p.PhotoId == request.PhotoId, cancellationToken);

        if (photo == null)
        {
            _logger.LogWarning("Photo {PhotoId} not found", request.PhotoId);
            return false;
        }

        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted photo {PhotoId}", request.PhotoId);

        return true;
    }
}
