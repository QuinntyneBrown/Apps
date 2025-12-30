// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class ApplicationTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesApplication()
    {
        var applicationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var companyId = Guid.NewGuid();
        var jobTitle = "Software Engineer";
        var status = ApplicationStatus.Applied;
        var appliedDate = new DateTime(2025, 1, 15);

        var application = new Application
        {
            ApplicationId = applicationId,
            UserId = userId,
            CompanyId = companyId,
            JobTitle = jobTitle,
            Status = status,
            AppliedDate = appliedDate
        };

        Assert.Multiple(() =>
        {
            Assert.That(application.ApplicationId, Is.EqualTo(applicationId));
            Assert.That(application.UserId, Is.EqualTo(userId));
            Assert.That(application.CompanyId, Is.EqualTo(companyId));
            Assert.That(application.JobTitle, Is.EqualTo(jobTitle));
            Assert.That(application.Status, Is.EqualTo(status));
            Assert.That(application.AppliedDate, Is.EqualTo(appliedDate));
            Assert.That(application.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void UpdateStatus_ValidStatus_UpdatesStatusAndTimestamp()
    {
        var application = new Application { Status = ApplicationStatus.Applied, UpdatedAt = null };

        application.UpdateStatus(ApplicationStatus.Interviewing);

        Assert.Multiple(() =>
        {
            Assert.That(application.Status, Is.EqualTo(ApplicationStatus.Interviewing));
            Assert.That(application.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsInFinalState_AcceptedStatus_ReturnsTrue()
    {
        var application = new Application { Status = ApplicationStatus.Accepted };
        Assert.That(application.IsInFinalState(), Is.True);
    }

    [Test]
    public void IsInFinalState_RejectedStatus_ReturnsTrue()
    {
        var application = new Application { Status = ApplicationStatus.Rejected };
        Assert.That(application.IsInFinalState(), Is.True);
    }

    [Test]
    public void IsInFinalState_WithdrawnStatus_ReturnsTrue()
    {
        var application = new Application { Status = ApplicationStatus.Withdrawn };
        Assert.That(application.IsInFinalState(), Is.True);
    }

    [Test]
    public void IsInFinalState_AppliedStatus_ReturnsFalse()
    {
        var application = new Application { Status = ApplicationStatus.Applied };
        Assert.That(application.IsInFinalState(), Is.False);
    }

    [Test]
    public void IsInFinalState_InterviewingStatus_ReturnsFalse()
    {
        var application = new Application { Status = ApplicationStatus.Interviewing };
        Assert.That(application.IsInFinalState(), Is.False);
    }

    [Test]
    public void Status_AllStatuses_CanBeAssigned()
    {
        var application = new Application();
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.Draft);
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.Applied);
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.UnderReview);
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.PhoneScreen);
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.Interviewing);
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.OfferReceived);
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.Accepted);
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.Rejected);
        Assert.DoesNotThrow(() => application.Status = ApplicationStatus.Withdrawn);
    }

    [Test]
    public void IsRemote_DefaultValue_IsFalse()
    {
        var application = new Application();
        Assert.That(application.IsRemote, Is.False);
    }

    [Test]
    public void NavigationProperties_CanBeSet()
    {
        var company = new Company { CompanyId = Guid.NewGuid() };
        var application = new Application();
        application.Company = company;
        Assert.That(application.Company, Is.EqualTo(company));
    }

    [Test]
    public void Interviews_DefaultValue_IsEmptyList()
    {
        var application = new Application();
        Assert.That(application.Interviews, Is.Not.Null);
        Assert.That(application.Interviews, Is.Empty);
    }
}
