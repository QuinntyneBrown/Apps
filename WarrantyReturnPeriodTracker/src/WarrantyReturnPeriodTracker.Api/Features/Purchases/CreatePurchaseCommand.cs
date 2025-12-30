using MediatR;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Purchases;

public class CreatePurchaseCommand : IRequest<PurchaseDto>
{
    public Guid UserId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public string? ModelNumber { get; set; }
    public string? Notes { get; set; }
}

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, PurchaseDto>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public CreatePurchaseCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<PurchaseDto> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = request.UserId,
            ProductName = request.ProductName,
            Category = request.Category,
            StoreName = request.StoreName,
            PurchaseDate = request.PurchaseDate,
            Price = request.Price,
            ModelNumber = request.ModelNumber,
            Notes = request.Notes,
            Status = PurchaseStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync(cancellationToken);

        return purchase.ToDto();
    }
}
