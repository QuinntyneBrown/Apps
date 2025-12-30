using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Receipts;

public class ReceiptDto
{
    public Guid ReceiptId { get; set; }
    public Guid PurchaseId { get; set; }
    public string ReceiptNumber { get; set; } = string.Empty;
    public ReceiptType ReceiptType { get; set; }
    public ReceiptFormat Format { get; set; }
    public string? StorageLocation { get; set; }
    public DateTime ReceiptDate { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public ReceiptStatus Status { get; set; }
    public bool IsVerified { get; set; }
    public string? Notes { get; set; }
    public DateTime UploadedAt { get; set; }
}

public static class ReceiptDtoExtensions
{
    public static ReceiptDto ToDto(this Receipt receipt)
    {
        return new ReceiptDto
        {
            ReceiptId = receipt.ReceiptId,
            PurchaseId = receipt.PurchaseId,
            ReceiptNumber = receipt.ReceiptNumber,
            ReceiptType = receipt.ReceiptType,
            Format = receipt.Format,
            StorageLocation = receipt.StorageLocation,
            ReceiptDate = receipt.ReceiptDate,
            StoreName = receipt.StoreName,
            TotalAmount = receipt.TotalAmount,
            PaymentMethod = receipt.PaymentMethod,
            Status = receipt.Status,
            IsVerified = receipt.IsVerified,
            Notes = receipt.Notes,
            UploadedAt = receipt.UploadedAt
        };
    }
}
