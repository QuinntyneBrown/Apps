using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Resources;

public record DeleteResourceCommand : IRequest<bool>
{
    public Guid ResourceId { get; init; }
}

public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand, bool>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<DeleteResourceCommandHandler> _logger;

    public DeleteResourceCommandHandler(
        IProfessionalReadingListContext context,
        ILogger<DeleteResourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting resource {ResourceId}", request.ResourceId);

        var resource = await _context.Resources
            .FirstOrDefaultAsync(r => r.ResourceId == request.ResourceId, cancellationToken);

        if (resource == null)
        {
            _logger.LogWarning("Resource {ResourceId} not found", request.ResourceId);
            return false;
        }

        _context.Resources.Remove(resource);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted resource {ResourceId}", request.ResourceId);

        return true;
    }
}
