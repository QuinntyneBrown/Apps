// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class CompanyTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCompany()
    {
        var companyId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "TechCorp";
        var industry = "Technology";
        var website = "https://techcorp.com";
        var location = "San Francisco, CA";

        var company = new Company
        {
            CompanyId = companyId,
            UserId = userId,
            Name = name,
            Industry = industry,
            Website = website,
            Location = location
        };

        Assert.Multiple(() =>
        {
            Assert.That(company.CompanyId, Is.EqualTo(companyId));
            Assert.That(company.UserId, Is.EqualTo(userId));
            Assert.That(company.Name, Is.EqualTo(name));
            Assert.That(company.Industry, Is.EqualTo(industry));
            Assert.That(company.Website, Is.EqualTo(website));
            Assert.That(company.Location, Is.EqualTo(location));
            Assert.That(company.IsTargetCompany, Is.False);
            Assert.That(company.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void MarkAsTarget_SetsIsTargetCompanyAndUpdatesTimestamp()
    {
        var company = new Company { IsTargetCompany = false, UpdatedAt = null };

        company.MarkAsTarget();

        Assert.Multiple(() =>
        {
            Assert.That(company.IsTargetCompany, Is.True);
            Assert.That(company.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void MarkAsTarget_AlreadyTarget_UpdatesTimestamp()
    {
        var oldUpdatedAt = new DateTime(2025, 1, 1);
        var company = new Company { IsTargetCompany = true, UpdatedAt = oldUpdatedAt };

        company.MarkAsTarget();

        Assert.Multiple(() =>
        {
            Assert.That(company.IsTargetCompany, Is.True);
            Assert.That(company.UpdatedAt, Is.GreaterThan(oldUpdatedAt));
        });
    }

    [Test]
    public void IsTargetCompany_DefaultValue_IsFalse()
    {
        var company = new Company();
        Assert.That(company.IsTargetCompany, Is.False);
    }

    [Test]
    public void Applications_DefaultValue_IsEmptyList()
    {
        var company = new Company();
        Assert.That(company.Applications, Is.Not.Null);
        Assert.That(company.Applications, Is.Empty);
    }

    [Test]
    public void OptionalFields_CanBeNull()
    {
        var company = new Company
        {
            Industry = null,
            Website = null,
            Location = null,
            CompanySize = null,
            CultureNotes = null,
            ResearchNotes = null
        };

        Assert.Multiple(() =>
        {
            Assert.That(company.Industry, Is.Null);
            Assert.That(company.Website, Is.Null);
            Assert.That(company.Location, Is.Null);
            Assert.That(company.CompanySize, Is.Null);
            Assert.That(company.CultureNotes, Is.Null);
            Assert.That(company.ResearchNotes, Is.Null);
        });
    }
}
