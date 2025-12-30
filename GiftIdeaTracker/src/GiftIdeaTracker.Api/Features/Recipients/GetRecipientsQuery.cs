using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Recipients;

public record GetRecipientsQuery : IRequest<IEnumerable<RecipientDto>>
{
    public Guid? UserId { get; init; }
}

public class GetRecipientsQueryHandler : IRequestHandler<GetRecipientsQuery, IEnumerable<RecipientDto>>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<GetRecipientsQueryHandler> _logger;

    public GetRecipientsQueryHandler(
        IGiftIdeaTrackerContext context,
        ILogger<GetRecipientsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<RecipientDto>> Handle(GetRecipientsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipients for user {UserId}", request.UserId);

        var query = _context.Recipients.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        var recipients = await query
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);

        return recipients.Select(r => r.ToDto());
    }
}
