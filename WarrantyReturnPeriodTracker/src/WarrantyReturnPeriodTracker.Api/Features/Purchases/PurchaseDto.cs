using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Purchases;

public class PurchaseDto
{
    public Guid PurchaseId { get; set; }
    public Guid UserId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public PurchaseStatus Status { get; set; }
    public string? ModelNumber { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public static class PurchaseDtoExtensions
{
    public static PurchaseDto ToDto(this Purchase purchase)
    {
        return new PurchaseDto
        {
            PurchaseId = purchase.PurchaseId,
            UserId = purchase.UserId,
            ProductName = purchase.ProductName,
            Category = purchase.Category,
            StoreName = purchase.StoreName,
            PurchaseDate = purchase.PurchaseDate,
            Price = purchase.Price,
            Status = purchase.Status,
            ModelNumber = purchase.ModelNumber,
            Notes = purchase.Notes,
            CreatedAt = purchase.CreatedAt
        };
    }
}
