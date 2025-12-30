using HomeInventoryManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.Items;

public record UpdateItemCommand : IRequest<ItemDto?>
{
    public Guid ItemId { get; init; }
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
    public int Quantity { get; init; }
    public string? PhotoUrl { get; init; }
    public string? ReceiptUrl { get; init; }
    public string? Notes { get; init; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ItemDto?>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<UpdateItemCommandHandler> _logger;

    public UpdateItemCommandHandler(
        IHomeInventoryManagerContext context,
        ILogger<UpdateItemCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ItemDto?> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating item {ItemId}", request.ItemId);

        var item = await _context.Items
            .FirstOrDefaultAsync(i => i.ItemId == request.ItemId, cancellationToken);

        if (item == null)
        {
            _logger.LogWarning("Item {ItemId} not found", request.ItemId);
            return null;
        }

        item.Name = request.Name;
        item.Description = request.Description;
        item.Category = request.Category;
        item.Room = request.Room;
        item.Brand = request.Brand;
        item.ModelNumber = request.ModelNumber;
        item.SerialNumber = request.SerialNumber;
        item.PurchaseDate = request.PurchaseDate;
        item.PurchasePrice = request.PurchasePrice;
        item.CurrentValue = request.CurrentValue;
        item.Quantity = request.Quantity;
        item.PhotoUrl = request.PhotoUrl;
        item.ReceiptUrl = request.ReceiptUrl;
        item.Notes = request.Notes;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated item {ItemId}", request.ItemId);

        return item.ToDto();
    }
}
