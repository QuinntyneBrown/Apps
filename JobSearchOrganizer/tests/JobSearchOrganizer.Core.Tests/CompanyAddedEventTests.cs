// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class CompanyAddedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCompanyAddedEvent()
    {
        var companyId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "TechCorp";
        var timestamp = DateTime.UtcNow;

        var evt = new CompanyAddedEvent
        {
            CompanyId = companyId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.CompanyId, Is.EqualTo(companyId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        var evt = new CompanyAddedEvent();
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var companyId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new CompanyAddedEvent { CompanyId = companyId, Name = "TechCorp", Timestamp = timestamp };
        var evt2 = new CompanyAddedEvent { CompanyId = companyId, Name = "TechCorp", Timestamp = timestamp };

        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new CompanyAddedEvent { CompanyId = Guid.NewGuid(), Name = "Original" };
        var modified = original with { Name = "Modified" };

        Assert.That(modified.Name, Is.EqualTo("Modified"));
        Assert.That(modified, Is.Not.SameAs(original));
    }
}
