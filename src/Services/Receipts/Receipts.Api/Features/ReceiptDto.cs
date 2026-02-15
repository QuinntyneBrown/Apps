using Receipts.Core.Models;

namespace Receipts.Api.Features;

public record ReceiptDto
{
    public Guid ReceiptId { get; init; }
    public Guid TenantId { get; init; }
    public Guid DeductionId { get; init; }
    public string FileName { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
    public long FileSize { get; init; }
    public DateTime UploadedAt { get; init; }
}

public static class ReceiptDtoExtensions
{
    public static ReceiptDto ToDto(this Receipt receipt) => new()
    {
        ReceiptId = receipt.ReceiptId,
        TenantId = receipt.TenantId,
        DeductionId = receipt.DeductionId,
        FileName = receipt.FileName,
        ContentType = receipt.ContentType,
        FileSize = receipt.FileSize,
        UploadedAt = receipt.UploadedAt
    };
}
