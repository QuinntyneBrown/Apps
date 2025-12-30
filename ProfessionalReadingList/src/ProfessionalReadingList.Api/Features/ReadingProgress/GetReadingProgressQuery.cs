using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.ReadingProgress;

public record GetReadingProgressQuery : IRequest<IEnumerable<ReadingProgressDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ResourceId { get; init; }
    public string? Status { get; init; }
}

public class GetReadingProgressQueryHandler : IRequestHandler<GetReadingProgressQuery, IEnumerable<ReadingProgressDto>>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<GetReadingProgressQueryHandler> _logger;

    public GetReadingProgressQueryHandler(
        IProfessionalReadingListContext context,
        ILogger<GetReadingProgressQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ReadingProgressDto>> Handle(GetReadingProgressQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reading progress for user {UserId}", request.UserId);

        var query = _context.ReadingProgress.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(rp => rp.UserId == request.UserId.Value);
        }

        if (request.ResourceId.HasValue)
        {
            query = query.Where(rp => rp.ResourceId == request.ResourceId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            query = query.Where(rp => rp.Status == request.Status);
        }

        var readingProgress = await query
            .OrderByDescending(rp => rp.CreatedAt)
            .ToListAsync(cancellationToken);

        return readingProgress.Select(rp => rp.ToDto());
    }
}
