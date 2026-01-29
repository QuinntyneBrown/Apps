namespace Receipts.Core.Models;

public class Receipt
{
    public Guid ReceiptId { get; set; }
    public Guid PurchaseId { get; set; }
    public Guid UserId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string? FilePath { get; set; }
    public string? ContentType { get; set; }
    public long? FileSize { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
