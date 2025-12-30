using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Messages;

public record UpdateMessageCommand : IRequest<MessageDto?>
{
    public Guid MessageId { get; init; }
    public string Subject { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public bool IsRead { get; init; }
}

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, MessageDto?>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<UpdateMessageCommandHandler> _logger;

    public UpdateMessageCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<UpdateMessageCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MessageDto?> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating message {MessageId}", request.MessageId);

        var message = await _context.Messages
            .FirstOrDefaultAsync(m => m.MessageId == request.MessageId, cancellationToken);

        if (message == null)
        {
            _logger.LogWarning("Message {MessageId} not found", request.MessageId);
            return null;
        }

        message.Subject = request.Subject;
        message.Content = request.Content;

        if (request.IsRead && !message.IsRead)
        {
            message.MarkAsRead();
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated message {MessageId}", request.MessageId);

        return message.ToDto();
    }
}
