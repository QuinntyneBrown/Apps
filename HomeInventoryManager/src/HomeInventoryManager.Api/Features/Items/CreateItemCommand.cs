using HomeInventoryManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.Items;

public record CreateItemCommand : IRequest<ItemDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Category Category { get; init; }
    public Room Room { get; init; }
    public string? Brand { get; init; }
    public string? ModelNumber { get; init; }
    public string? SerialNumber { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public decimal? PurchasePrice { get; init; }
    public decimal? CurrentValue { get; init; }
    public int Quantity { get; init; } = 1;
    public string? PhotoUrl { get; init; }
    public string? ReceiptUrl { get; init; }
    public string? Notes { get; init; }
}

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemDto>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<CreateItemCommandHandler> _logger;

    public CreateItemCommandHandler(
        IHomeInventoryManagerContext context,
        ILogger<CreateItemCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ItemDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating item for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Category = request.Category,
            Room = request.Room,
            Brand = request.Brand,
            ModelNumber = request.ModelNumber,
            SerialNumber = request.SerialNumber,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            CurrentValue = request.CurrentValue,
            Quantity = request.Quantity,
            PhotoUrl = request.PhotoUrl,
            ReceiptUrl = request.ReceiptUrl,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.Items.Add(item);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created item {ItemId} for user {UserId}",
            item.ItemId,
            request.UserId);

        return item.ToDto();
    }
}
