using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Tags;

public record UpdateTagCommand : IRequest<TagDto?>
{
    public Guid TagId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Color { get; init; }
}

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, TagDto?>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<UpdateTagCommandHandler> _logger;

    public UpdateTagCommandHandler(
        IDailyJournalingAppContext context,
        ILogger<UpdateTagCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TagDto?> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating tag {TagId}", request.TagId);

        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.TagId == request.TagId, cancellationToken);

        if (tag == null)
        {
            _logger.LogWarning("Tag {TagId} not found", request.TagId);
            return null;
        }

        tag.Name = request.Name;
        tag.Color = request.Color;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated tag {TagId}", request.TagId);

        return tag.ToDto();
    }
}
