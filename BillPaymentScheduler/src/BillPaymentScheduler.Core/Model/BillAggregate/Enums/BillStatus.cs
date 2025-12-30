// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core;

public enum BillStatus
{
    Pending = 0,
    Paid = 1,
    Overdue = 2,
    Cancelled = 3,
}
