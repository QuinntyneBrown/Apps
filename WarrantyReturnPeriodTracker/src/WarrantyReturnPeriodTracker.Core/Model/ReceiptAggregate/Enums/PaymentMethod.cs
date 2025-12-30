// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the payment method used for a purchase.
/// </summary>
public enum PaymentMethod
{
    /// <summary>
    /// Cash payment.
    /// </summary>
    Cash = 0,

    /// <summary>
    /// Credit card payment.
    /// </summary>
    CreditCard = 1,

    /// <summary>
    /// Debit card payment.
    /// </summary>
    DebitCard = 2,

    /// <summary>
    /// PayPal or similar online payment.
    /// </summary>
    PayPal = 3,

    /// <summary>
    /// Bank transfer or wire.
    /// </summary>
    BankTransfer = 4,

    /// <summary>
    /// Digital wallet (Apple Pay, Google Pay, etc.).
    /// </summary>
    DigitalWallet = 5,

    /// <summary>
    /// Check payment.
    /// </summary>
    Check = 6,

    /// <summary>
    /// Other or unknown payment method.
    /// </summary>
    Other = 7,
}
