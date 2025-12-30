// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class ApplicationStatusTests
{
    [Test]
    public void ApplicationStatus_DraftValue_EqualsZero()
    {
        Assert.That((int)ApplicationStatus.Draft, Is.EqualTo(0));
    }

    [Test]
    public void ApplicationStatus_AppliedValue_EqualsOne()
    {
        Assert.That((int)ApplicationStatus.Applied, Is.EqualTo(1));
    }

    [Test]
    public void ApplicationStatus_UnderReviewValue_EqualsTwo()
    {
        Assert.That((int)ApplicationStatus.UnderReview, Is.EqualTo(2));
    }

    [Test]
    public void ApplicationStatus_PhoneScreenValue_EqualsThree()
    {
        Assert.That((int)ApplicationStatus.PhoneScreen, Is.EqualTo(3));
    }

    [Test]
    public void ApplicationStatus_InterviewingValue_EqualsFour()
    {
        Assert.That((int)ApplicationStatus.Interviewing, Is.EqualTo(4));
    }

    [Test]
    public void ApplicationStatus_OfferReceivedValue_EqualsFive()
    {
        Assert.That((int)ApplicationStatus.OfferReceived, Is.EqualTo(5));
    }

    [Test]
    public void ApplicationStatus_AcceptedValue_EqualsSix()
    {
        Assert.That((int)ApplicationStatus.Accepted, Is.EqualTo(6));
    }

    [Test]
    public void ApplicationStatus_RejectedValue_EqualsSeven()
    {
        Assert.That((int)ApplicationStatus.Rejected, Is.EqualTo(7));
    }

    [Test]
    public void ApplicationStatus_WithdrawnValue_EqualsEight()
    {
        Assert.That((int)ApplicationStatus.Withdrawn, Is.EqualTo(8));
    }

    [Test]
    public void ApplicationStatus_AllValues_CanBeAssigned()
    {
        ApplicationStatus status;
        Assert.DoesNotThrow(() => status = ApplicationStatus.Draft);
        Assert.DoesNotThrow(() => status = ApplicationStatus.Applied);
        Assert.DoesNotThrow(() => status = ApplicationStatus.UnderReview);
        Assert.DoesNotThrow(() => status = ApplicationStatus.PhoneScreen);
        Assert.DoesNotThrow(() => status = ApplicationStatus.Interviewing);
        Assert.DoesNotThrow(() => status = ApplicationStatus.OfferReceived);
        Assert.DoesNotThrow(() => status = ApplicationStatus.Accepted);
        Assert.DoesNotThrow(() => status = ApplicationStatus.Rejected);
        Assert.DoesNotThrow(() => status = ApplicationStatus.Withdrawn);
    }

    [Test]
    public void ApplicationStatus_DefaultValue_IsDraft()
    {
        ApplicationStatus status = default;
        Assert.That(status, Is.EqualTo(ApplicationStatus.Draft));
    }
}
