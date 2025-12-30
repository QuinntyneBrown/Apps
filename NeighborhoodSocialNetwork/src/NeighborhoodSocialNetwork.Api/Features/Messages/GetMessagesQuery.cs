using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Messages;

public record GetMessagesQuery : IRequest<IEnumerable<MessageDto>>
{
    public Guid? SenderNeighborId { get; init; }
    public Guid? RecipientNeighborId { get; init; }
    public bool? IsRead { get; init; }
}

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<MessageDto>>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<GetMessagesQueryHandler> _logger;

    public GetMessagesQueryHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<GetMessagesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting messages");

        var query = _context.Messages.AsNoTracking();

        if (request.SenderNeighborId.HasValue)
        {
            query = query.Where(m => m.SenderNeighborId == request.SenderNeighborId.Value);
        }

        if (request.RecipientNeighborId.HasValue)
        {
            query = query.Where(m => m.RecipientNeighborId == request.RecipientNeighborId.Value);
        }

        if (request.IsRead.HasValue)
        {
            query = query.Where(m => m.IsRead == request.IsRead.Value);
        }

        var messages = await query
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);

        return messages.Select(m => m.ToDto());
    }
}
