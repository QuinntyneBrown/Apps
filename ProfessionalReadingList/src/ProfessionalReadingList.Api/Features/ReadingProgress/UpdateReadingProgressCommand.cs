using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.ReadingProgress;

public record UpdateReadingProgressCommand : IRequest<ReadingProgressDto?>
{
    public Guid ReadingProgressId { get; init; }
    public string Status { get; init; } = "Not Started";
    public int? CurrentPage { get; init; }
    public int ProgressPercentage { get; init; }
    public int? Rating { get; init; }
    public string? Review { get; init; }
}

public class UpdateReadingProgressCommandHandler : IRequestHandler<UpdateReadingProgressCommand, ReadingProgressDto?>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<UpdateReadingProgressCommandHandler> _logger;

    public UpdateReadingProgressCommandHandler(
        IProfessionalReadingListContext context,
        ILogger<UpdateReadingProgressCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReadingProgressDto?> Handle(UpdateReadingProgressCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reading progress {ReadingProgressId}", request.ReadingProgressId);

        var readingProgress = await _context.ReadingProgress
            .FirstOrDefaultAsync(rp => rp.ReadingProgressId == request.ReadingProgressId, cancellationToken);

        if (readingProgress == null)
        {
            _logger.LogWarning("Reading progress {ReadingProgressId} not found", request.ReadingProgressId);
            return null;
        }

        readingProgress.Status = request.Status;
        readingProgress.CurrentPage = request.CurrentPage;
        readingProgress.ProgressPercentage = request.ProgressPercentage;
        readingProgress.Rating = request.Rating;
        readingProgress.Review = request.Review;
        readingProgress.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated reading progress {ReadingProgressId}", request.ReadingProgressId);

        return readingProgress.ToDto();
    }
}
