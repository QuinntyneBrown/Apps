// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core;

public record ReceiptUploadedEvent
{
    public Guid ReceiptId { get; init; }
    public Guid DeductionId { get; init; }
    public string FileName { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
