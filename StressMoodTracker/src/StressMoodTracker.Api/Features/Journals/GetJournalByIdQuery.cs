using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Journals;

public record GetJournalByIdQuery : IRequest<JournalDto?>
{
    public Guid JournalId { get; init; }
}

public class GetJournalByIdQueryHandler : IRequestHandler<GetJournalByIdQuery, JournalDto?>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<GetJournalByIdQueryHandler> _logger;

    public GetJournalByIdQueryHandler(
        IStressMoodTrackerContext context,
        ILogger<GetJournalByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<JournalDto?> Handle(GetJournalByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting journal entry {JournalId}", request.JournalId);

        var journal = await _context.Journals
            .AsNoTracking()
            .FirstOrDefaultAsync(j => j.JournalId == request.JournalId, cancellationToken);

        return journal?.ToDto();
    }
}
