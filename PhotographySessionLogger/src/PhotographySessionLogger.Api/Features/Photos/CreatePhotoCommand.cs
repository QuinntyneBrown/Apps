using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Photos;

public record CreatePhotoCommand : IRequest<PhotoDto>
{
    public Guid UserId { get; init; }
    public Guid SessionId { get; init; }
    public string FileName { get; init; } = string.Empty;
    public string? FilePath { get; init; }
    public string? CameraSettings { get; init; }
    public int? Rating { get; init; }
    public string? Tags { get; init; }
}

public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, PhotoDto>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<CreatePhotoCommandHandler> _logger;

    public CreatePhotoCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<CreatePhotoCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PhotoDto> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating photo for user {UserId}, session: {SessionId}, file: {FileName}",
            request.UserId,
            request.SessionId,
            request.FileName);

        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = request.UserId,
            SessionId = request.SessionId,
            FileName = request.FileName,
            FilePath = request.FilePath,
            CameraSettings = request.CameraSettings,
            Rating = request.Rating,
            Tags = request.Tags,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created photo {PhotoId} for user {UserId}",
            photo.PhotoId,
            request.UserId);

        return photo.ToDto();
    }
}
