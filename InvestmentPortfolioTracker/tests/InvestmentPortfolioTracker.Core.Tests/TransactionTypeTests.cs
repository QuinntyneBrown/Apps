// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class TransactionTypeTests
{
    [Test]
    public void TransactionType_BuyValue_EqualsZero()
    {
        Assert.That((int)TransactionType.Buy, Is.EqualTo(0));
    }

    [Test]
    public void TransactionType_SellValue_EqualsOne()
    {
        Assert.That((int)TransactionType.Sell, Is.EqualTo(1));
    }

    [Test]
    public void TransactionType_DividendValue_EqualsTwo()
    {
        Assert.That((int)TransactionType.Dividend, Is.EqualTo(2));
    }

    [Test]
    public void TransactionType_InterestValue_EqualsThree()
    {
        Assert.That((int)TransactionType.Interest, Is.EqualTo(3));
    }

    [Test]
    public void TransactionType_DepositValue_EqualsFour()
    {
        Assert.That((int)TransactionType.Deposit, Is.EqualTo(4));
    }

    [Test]
    public void TransactionType_WithdrawalValue_EqualsFive()
    {
        Assert.That((int)TransactionType.Withdrawal, Is.EqualTo(5));
    }

    [Test]
    public void TransactionType_TransferValue_EqualsSix()
    {
        Assert.That((int)TransactionType.Transfer, Is.EqualTo(6));
    }

    [Test]
    public void TransactionType_FeeValue_EqualsSeven()
    {
        Assert.That((int)TransactionType.Fee, Is.EqualTo(7));
    }

    [Test]
    public void TransactionType_AllValues_CanBeAssigned()
    {
        TransactionType type;
        Assert.DoesNotThrow(() => type = TransactionType.Buy);
        Assert.DoesNotThrow(() => type = TransactionType.Sell);
        Assert.DoesNotThrow(() => type = TransactionType.Dividend);
        Assert.DoesNotThrow(() => type = TransactionType.Interest);
        Assert.DoesNotThrow(() => type = TransactionType.Deposit);
        Assert.DoesNotThrow(() => type = TransactionType.Withdrawal);
        Assert.DoesNotThrow(() => type = TransactionType.Transfer);
        Assert.DoesNotThrow(() => type = TransactionType.Fee);
    }

    [Test]
    public void TransactionType_DefaultValue_IsBuy()
    {
        TransactionType type = default;
        Assert.That(type, Is.EqualTo(TransactionType.Buy));
    }
}
