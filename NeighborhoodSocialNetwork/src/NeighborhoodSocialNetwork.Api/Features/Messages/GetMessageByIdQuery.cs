using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Messages;

public record GetMessageByIdQuery : IRequest<MessageDto?>
{
    public Guid MessageId { get; init; }
}

public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, MessageDto?>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<GetMessageByIdQueryHandler> _logger;

    public GetMessageByIdQueryHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<GetMessageByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MessageDto?> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting message {MessageId}", request.MessageId);

        var message = await _context.Messages
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MessageId == request.MessageId, cancellationToken);

        if (message == null)
        {
            _logger.LogWarning("Message {MessageId} not found", request.MessageId);
            return null;
        }

        return message.ToDto();
    }
}
