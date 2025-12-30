// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Api.Features.Bankrolls;

public class BankrollDto
{
    public Guid BankrollId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime RecordedDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
