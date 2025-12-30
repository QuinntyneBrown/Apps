using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Tags;

public record DeleteTagCommand : IRequest<bool>
{
    public Guid TagId { get; init; }
}

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, bool>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<DeleteTagCommandHandler> _logger;

    public DeleteTagCommandHandler(
        IDailyJournalingAppContext context,
        ILogger<DeleteTagCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting tag {TagId}", request.TagId);

        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.TagId == request.TagId, cancellationToken);

        if (tag == null)
        {
            _logger.LogWarning("Tag {TagId} not found", request.TagId);
            return false;
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted tag {TagId}", request.TagId);

        return true;
    }
}
