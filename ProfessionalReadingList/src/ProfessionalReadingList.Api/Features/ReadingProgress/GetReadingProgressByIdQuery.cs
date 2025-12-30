using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.ReadingProgress;

public record GetReadingProgressByIdQuery : IRequest<ReadingProgressDto?>
{
    public Guid ReadingProgressId { get; init; }
}

public class GetReadingProgressByIdQueryHandler : IRequestHandler<GetReadingProgressByIdQuery, ReadingProgressDto?>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<GetReadingProgressByIdQueryHandler> _logger;

    public GetReadingProgressByIdQueryHandler(
        IProfessionalReadingListContext context,
        ILogger<GetReadingProgressByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReadingProgressDto?> Handle(GetReadingProgressByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reading progress {ReadingProgressId}", request.ReadingProgressId);

        var readingProgress = await _context.ReadingProgress
            .AsNoTracking()
            .FirstOrDefaultAsync(rp => rp.ReadingProgressId == request.ReadingProgressId, cancellationToken);

        return readingProgress?.ToDto();
    }
}
