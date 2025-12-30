// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core.Tests;

public class InjuryTypeTests
{
    [Test]
    public void InjuryType_StrainValue_EqualsZero()
    {
        Assert.That((int)InjuryType.Strain, Is.EqualTo(0));
    }

    [Test]
    public void InjuryType_SprainValue_EqualsOne()
    {
        Assert.That((int)InjuryType.Sprain, Is.EqualTo(1));
    }

    [Test]
    public void InjuryType_FractureValue_EqualsTwo()
    {
        Assert.That((int)InjuryType.Fracture, Is.EqualTo(2));
    }

    [Test]
    public void InjuryType_TendonitisValue_EqualsThree()
    {
        Assert.That((int)InjuryType.Tendonitis, Is.EqualTo(3));
    }

    [Test]
    public void InjuryType_CartilageDamageValue_EqualsFour()
    {
        Assert.That((int)InjuryType.CartilageDamage, Is.EqualTo(4));
    }

    [Test]
    public void InjuryType_OveruseValue_EqualsFive()
    {
        Assert.That((int)InjuryType.Overuse, Is.EqualTo(5));
    }

    [Test]
    public void InjuryType_OtherValue_EqualsSix()
    {
        Assert.That((int)InjuryType.Other, Is.EqualTo(6));
    }

    [Test]
    public void InjuryType_AllValues_CanBeAssigned()
    {
        InjuryType type;
        Assert.DoesNotThrow(() => type = InjuryType.Strain);
        Assert.DoesNotThrow(() => type = InjuryType.Sprain);
        Assert.DoesNotThrow(() => type = InjuryType.Fracture);
        Assert.DoesNotThrow(() => type = InjuryType.Tendonitis);
        Assert.DoesNotThrow(() => type = InjuryType.CartilageDamage);
        Assert.DoesNotThrow(() => type = InjuryType.Overuse);
        Assert.DoesNotThrow(() => type = InjuryType.Other);
    }

    [Test]
    public void InjuryType_DefaultValue_IsStrain()
    {
        InjuryType type = default;
        Assert.That(type, Is.EqualTo(InjuryType.Strain));
    }
}
