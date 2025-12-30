using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.ReadingProgress;

public record CreateReadingProgressCommand : IRequest<ReadingProgressDto>
{
    public Guid UserId { get; init; }
    public Guid ResourceId { get; init; }
    public string Status { get; init; } = "Not Started";
    public int? CurrentPage { get; init; }
    public int ProgressPercentage { get; init; }
}

public class CreateReadingProgressCommandHandler : IRequestHandler<CreateReadingProgressCommand, ReadingProgressDto>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<CreateReadingProgressCommandHandler> _logger;

    public CreateReadingProgressCommandHandler(
        IProfessionalReadingListContext context,
        ILogger<CreateReadingProgressCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReadingProgressDto> Handle(CreateReadingProgressCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating reading progress for user {UserId}, resource {ResourceId}",
            request.UserId,
            request.ResourceId);

        var readingProgress = new Core.ReadingProgress
        {
            ReadingProgressId = Guid.NewGuid(),
            UserId = request.UserId,
            ResourceId = request.ResourceId,
            Status = request.Status,
            CurrentPage = request.CurrentPage,
            ProgressPercentage = request.ProgressPercentage,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ReadingProgress.Add(readingProgress);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created reading progress {ReadingProgressId} for user {UserId}",
            readingProgress.ReadingProgressId,
            request.UserId);

        return readingProgress.ToDto();
    }
}
