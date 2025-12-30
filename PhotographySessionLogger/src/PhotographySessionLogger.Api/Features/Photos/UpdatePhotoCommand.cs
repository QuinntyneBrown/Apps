using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Photos;

public record UpdatePhotoCommand : IRequest<PhotoDto?>
{
    public Guid PhotoId { get; init; }
    public string FileName { get; init; } = string.Empty;
    public string? FilePath { get; init; }
    public string? CameraSettings { get; init; }
    public int? Rating { get; init; }
    public string? Tags { get; init; }
}

public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, PhotoDto?>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<UpdatePhotoCommandHandler> _logger;

    public UpdatePhotoCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<UpdatePhotoCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PhotoDto?> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating photo {PhotoId}", request.PhotoId);

        var photo = await _context.Photos
            .FirstOrDefaultAsync(p => p.PhotoId == request.PhotoId, cancellationToken);

        if (photo == null)
        {
            _logger.LogWarning("Photo {PhotoId} not found", request.PhotoId);
            return null;
        }

        photo.FileName = request.FileName;
        photo.FilePath = request.FilePath;
        photo.CameraSettings = request.CameraSettings;
        photo.Rating = request.Rating;
        photo.Tags = request.Tags;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated photo {PhotoId}", request.PhotoId);

        return photo.ToDto();
    }
}
