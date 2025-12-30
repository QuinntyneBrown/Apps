// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class ContactTypeTests
{
    [Test]
    public void ContactType_Colleague_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.Colleague;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.Colleague));
        Assert.That((int)contactType, Is.EqualTo(0));
    }

    [Test]
    public void ContactType_Mentor_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.Mentor;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.Mentor));
        Assert.That((int)contactType, Is.EqualTo(1));
    }

    [Test]
    public void ContactType_Mentee_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.Mentee;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.Mentee));
        Assert.That((int)contactType, Is.EqualTo(2));
    }

    [Test]
    public void ContactType_Client_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.Client;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.Client));
        Assert.That((int)contactType, Is.EqualTo(3));
    }

    [Test]
    public void ContactType_Recruiter_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.Recruiter;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.Recruiter));
        Assert.That((int)contactType, Is.EqualTo(4));
    }

    [Test]
    public void ContactType_IndustryPeer_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.IndustryPeer;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.IndustryPeer));
        Assert.That((int)contactType, Is.EqualTo(5));
    }

    [Test]
    public void ContactType_Referral_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.Referral;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.Referral));
        Assert.That((int)contactType, Is.EqualTo(6));
    }

    [Test]
    public void ContactType_EventConnection_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.EventConnection;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.EventConnection));
        Assert.That((int)contactType, Is.EqualTo(7));
    }

    [Test]
    public void ContactType_Other_CanBeAssigned()
    {
        // Arrange & Act
        var contactType = ContactType.Other;

        // Assert
        Assert.That(contactType, Is.EqualTo(ContactType.Other));
        Assert.That((int)contactType, Is.EqualTo(8));
    }

    [Test]
    public void ContactType_AllValues_AreUnique()
    {
        // Arrange
        var values = Enum.GetValues<ContactType>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }

    [Test]
    public void ContactType_HasExpectedNumberOfValues()
    {
        // Arrange & Act
        var values = Enum.GetValues<ContactType>();

        // Assert
        Assert.That(values.Length, Is.EqualTo(9));
    }
}
