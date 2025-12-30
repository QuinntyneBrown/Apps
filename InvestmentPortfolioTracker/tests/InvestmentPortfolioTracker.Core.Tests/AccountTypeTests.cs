// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class AccountTypeTests
{
    [Test]
    public void AccountType_TaxableValue_EqualsZero()
    {
        Assert.That((int)AccountType.Taxable, Is.EqualTo(0));
    }

    [Test]
    public void AccountType_TraditionalIRAValue_EqualsOne()
    {
        Assert.That((int)AccountType.TraditionalIRA, Is.EqualTo(1));
    }

    [Test]
    public void AccountType_RothIRAValue_EqualsTwo()
    {
        Assert.That((int)AccountType.RothIRA, Is.EqualTo(2));
    }

    [Test]
    public void AccountType_FourZeroOneKValue_EqualsThree()
    {
        Assert.That((int)AccountType.FourZeroOneK, Is.EqualTo(3));
    }

    [Test]
    public void AccountType_FourZeroThreeBValue_EqualsFour()
    {
        Assert.That((int)AccountType.FourZeroThreeB, Is.EqualTo(4));
    }

    [Test]
    public void AccountType_HSAValue_EqualsFive()
    {
        Assert.That((int)AccountType.HSA, Is.EqualTo(5));
    }

    [Test]
    public void AccountType_FiveTwoNineValue_EqualsSix()
    {
        Assert.That((int)AccountType.FiveTwoNine, Is.EqualTo(6));
    }

    [Test]
    public void AccountType_AllValues_CanBeAssigned()
    {
        AccountType type;
        Assert.DoesNotThrow(() => type = AccountType.Taxable);
        Assert.DoesNotThrow(() => type = AccountType.TraditionalIRA);
        Assert.DoesNotThrow(() => type = AccountType.RothIRA);
        Assert.DoesNotThrow(() => type = AccountType.FourZeroOneK);
        Assert.DoesNotThrow(() => type = AccountType.FourZeroThreeB);
        Assert.DoesNotThrow(() => type = AccountType.HSA);
        Assert.DoesNotThrow(() => type = AccountType.FiveTwoNine);
    }

    [Test]
    public void AccountType_DefaultValue_IsTaxable()
    {
        AccountType type = default;
        Assert.That(type, Is.EqualTo(AccountType.Taxable));
    }
}
