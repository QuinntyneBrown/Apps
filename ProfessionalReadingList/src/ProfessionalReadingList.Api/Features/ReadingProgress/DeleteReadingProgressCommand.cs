using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.ReadingProgress;

public record DeleteReadingProgressCommand : IRequest<bool>
{
    public Guid ReadingProgressId { get; init; }
}

public class DeleteReadingProgressCommandHandler : IRequestHandler<DeleteReadingProgressCommand, bool>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<DeleteReadingProgressCommandHandler> _logger;

    public DeleteReadingProgressCommandHandler(
        IProfessionalReadingListContext context,
        ILogger<DeleteReadingProgressCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteReadingProgressCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reading progress {ReadingProgressId}", request.ReadingProgressId);

        var readingProgress = await _context.ReadingProgress
            .FirstOrDefaultAsync(rp => rp.ReadingProgressId == request.ReadingProgressId, cancellationToken);

        if (readingProgress == null)
        {
            _logger.LogWarning("Reading progress {ReadingProgressId} not found", request.ReadingProgressId);
            return false;
        }

        _context.ReadingProgress.Remove(readingProgress);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted reading progress {ReadingProgressId}", request.ReadingProgressId);

        return true;
    }
}
