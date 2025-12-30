// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core.Tests;

public class InjurySeverityTests
{
    [Test]
    public void InjurySeverity_MinorValue_EqualsZero()
    {
        Assert.That((int)InjurySeverity.Minor, Is.EqualTo(0));
    }

    [Test]
    public void InjurySeverity_ModerateValue_EqualsOne()
    {
        Assert.That((int)InjurySeverity.Moderate, Is.EqualTo(1));
    }

    [Test]
    public void InjurySeverity_SevereValue_EqualsTwo()
    {
        Assert.That((int)InjurySeverity.Severe, Is.EqualTo(2));
    }

    [Test]
    public void InjurySeverity_CriticalValue_EqualsThree()
    {
        Assert.That((int)InjurySeverity.Critical, Is.EqualTo(3));
    }

    [Test]
    public void InjurySeverity_AllValues_CanBeAssigned()
    {
        InjurySeverity severity;
        Assert.DoesNotThrow(() => severity = InjurySeverity.Minor);
        Assert.DoesNotThrow(() => severity = InjurySeverity.Moderate);
        Assert.DoesNotThrow(() => severity = InjurySeverity.Severe);
        Assert.DoesNotThrow(() => severity = InjurySeverity.Critical);
    }

    [Test]
    public void InjurySeverity_DefaultValue_IsMinor()
    {
        InjurySeverity severity = default;
        Assert.That(severity, Is.EqualTo(InjurySeverity.Minor));
    }

    [Test]
    public void InjurySeverity_CanCompareValues()
    {
        var minor = InjurySeverity.Minor;
        var severe = InjurySeverity.Severe;

        Assert.Multiple(() =>
        {
            Assert.That(minor, Is.Not.EqualTo(severe));
            Assert.That(minor, Is.EqualTo(InjurySeverity.Minor));
        });
    }
}
