// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class AchievementTypeTests
{
    [Test]
    public void AchievementType_Award_CanBeAssigned()
    {
        var type = AchievementType.Award;
        Assert.That(type, Is.EqualTo(AchievementType.Award));
        Assert.That((int)type, Is.EqualTo(0));
    }

    [Test]
    public void AchievementType_Certification_CanBeAssigned()
    {
        var type = AchievementType.Certification;
        Assert.That(type, Is.EqualTo(AchievementType.Certification));
        Assert.That((int)type, Is.EqualTo(1));
    }

    [Test]
    public void AchievementType_Publication_CanBeAssigned()
    {
        var type = AchievementType.Publication;
        Assert.That(type, Is.EqualTo(AchievementType.Publication));
        Assert.That((int)type, Is.EqualTo(2));
    }

    [Test]
    public void AchievementType_Presentation_CanBeAssigned()
    {
        var type = AchievementType.Presentation;
        Assert.That(type, Is.EqualTo(AchievementType.Presentation));
        Assert.That((int)type, Is.EqualTo(3));
    }

    [Test]
    public void AchievementType_ProjectMilestone_CanBeAssigned()
    {
        var type = AchievementType.ProjectMilestone;
        Assert.That(type, Is.EqualTo(AchievementType.ProjectMilestone));
        Assert.That((int)type, Is.EqualTo(4));
    }

    [Test]
    public void AchievementType_Promotion_CanBeAssigned()
    {
        var type = AchievementType.Promotion;
        Assert.That(type, Is.EqualTo(AchievementType.Promotion));
        Assert.That((int)type, Is.EqualTo(5));
    }

    [Test]
    public void AchievementType_FinancialImpact_CanBeAssigned()
    {
        var type = AchievementType.FinancialImpact;
        Assert.That(type, Is.EqualTo(AchievementType.FinancialImpact));
        Assert.That((int)type, Is.EqualTo(6));
    }

    [Test]
    public void AchievementType_Leadership_CanBeAssigned()
    {
        var type = AchievementType.Leadership;
        Assert.That(type, Is.EqualTo(AchievementType.Leadership));
        Assert.That((int)type, Is.EqualTo(7));
    }

    [Test]
    public void AchievementType_Innovation_CanBeAssigned()
    {
        var type = AchievementType.Innovation;
        Assert.That(type, Is.EqualTo(AchievementType.Innovation));
        Assert.That((int)type, Is.EqualTo(8));
    }

    [Test]
    public void AchievementType_Other_CanBeAssigned()
    {
        var type = AchievementType.Other;
        Assert.That(type, Is.EqualTo(AchievementType.Other));
        Assert.That((int)type, Is.EqualTo(9));
    }

    [Test]
    public void AchievementType_AllValues_AreUnique()
    {
        var values = Enum.GetValues<AchievementType>();
        var uniqueValues = values.Distinct().ToList();
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }

    [Test]
    public void AchievementType_HasExpectedNumberOfValues()
    {
        var values = Enum.GetValues<AchievementType>();
        Assert.That(values.Length, Is.EqualTo(10));
    }
}
