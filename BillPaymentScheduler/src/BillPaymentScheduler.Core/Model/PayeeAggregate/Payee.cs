// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core;

public class Payee
{
    public Guid PayeeId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? AccountNumber { get; set; }
    public string? Website { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Notes { get; set; }
    public List<Bill> Bills { get; set; } = new List<Bill>();
}
