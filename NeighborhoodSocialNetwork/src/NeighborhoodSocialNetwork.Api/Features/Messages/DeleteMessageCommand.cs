using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Messages;

public record DeleteMessageCommand : IRequest<bool>
{
    public Guid MessageId { get; init; }
}

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<DeleteMessageCommandHandler> _logger;

    public DeleteMessageCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<DeleteMessageCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting message {MessageId}", request.MessageId);

        var message = await _context.Messages
            .FirstOrDefaultAsync(m => m.MessageId == request.MessageId, cancellationToken);

        if (message == null)
        {
            _logger.LogWarning("Message {MessageId} not found", request.MessageId);
            return false;
        }

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted message {MessageId}", request.MessageId);

        return true;
    }
}
