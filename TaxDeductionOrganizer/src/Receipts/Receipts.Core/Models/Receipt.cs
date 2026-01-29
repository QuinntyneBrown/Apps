namespace Receipts.Core.Models;

public class Receipt
{
    public Guid ReceiptId { get; set; }
    public Guid TenantId { get; set; }
    public Guid DeductionId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string? StoragePath { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
