// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Receipts;

public class ReceiptDto
{
    public Guid ReceiptId { get; set; }
    public Guid DeductionId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; }
    public string? Notes { get; set; }

    public static ReceiptDto ToDto(Receipt receipt)
    {
        return new ReceiptDto
        {
            ReceiptId = receipt.ReceiptId,
            DeductionId = receipt.DeductionId,
            FileName = receipt.FileName,
            FileUrl = receipt.FileUrl,
            UploadDate = receipt.UploadDate,
            Notes = receipt.Notes
        };
    }
}

public static class ReceiptExtensions
{
    public static ReceiptDto ToDto(this Receipt receipt) => ReceiptDto.ToDto(receipt);
}
