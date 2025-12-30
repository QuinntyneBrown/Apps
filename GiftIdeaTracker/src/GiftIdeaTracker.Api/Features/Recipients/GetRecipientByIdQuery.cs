using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Recipients;

public record GetRecipientByIdQuery : IRequest<RecipientDto?>
{
    public Guid RecipientId { get; init; }
}

public class GetRecipientByIdQueryHandler : IRequestHandler<GetRecipientByIdQuery, RecipientDto?>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<GetRecipientByIdQueryHandler> _logger;

    public GetRecipientByIdQueryHandler(
        IGiftIdeaTrackerContext context,
        ILogger<GetRecipientByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecipientDto?> Handle(GetRecipientByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipient {RecipientId}", request.RecipientId);

        var recipient = await _context.Recipients
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RecipientId == request.RecipientId, cancellationToken);

        return recipient?.ToDto();
    }
}
