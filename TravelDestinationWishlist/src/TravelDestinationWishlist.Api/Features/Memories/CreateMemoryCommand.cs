using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Memories;

public record CreateMemoryCommand : IRequest<MemoryDto>
{
    public Guid UserId { get; init; }
    public Guid TripId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime MemoryDate { get; init; }
    public string? PhotoUrl { get; init; }
}

public class CreateMemoryCommandHandler : IRequestHandler<CreateMemoryCommand, MemoryDto>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<CreateMemoryCommandHandler> _logger;

    public CreateMemoryCommandHandler(
        ITravelDestinationWishlistContext context,
        ILogger<CreateMemoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MemoryDto> Handle(CreateMemoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating memory for user {UserId}, trip: {TripId}, title: {Title}",
            request.UserId,
            request.TripId,
            request.Title);

        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = request.UserId,
            TripId = request.TripId,
            Title = request.Title,
            Description = request.Description,
            MemoryDate = request.MemoryDate,
            PhotoUrl = request.PhotoUrl,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Memories.Add(memory);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created memory {MemoryId} for user {UserId}",
            memory.MemoryId,
            request.UserId);

        return memory.ToDto();
    }
}
