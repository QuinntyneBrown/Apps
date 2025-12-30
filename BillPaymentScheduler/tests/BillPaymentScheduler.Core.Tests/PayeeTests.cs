// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core.Tests;

public class PayeeTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPayee()
    {
        // Arrange
        var payeeId = Guid.NewGuid();
        var name = "Electric Company";
        var accountNumber = "ACC-12345";
        var website = "https://electriccompany.com";
        var phoneNumber = "555-1234";
        var notes = "Autopay setup";

        // Act
        var payee = new Payee
        {
            PayeeId = payeeId,
            Name = name,
            AccountNumber = accountNumber,
            Website = website,
            PhoneNumber = phoneNumber,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(payee.PayeeId, Is.EqualTo(payeeId));
            Assert.That(payee.Name, Is.EqualTo(name));
            Assert.That(payee.AccountNumber, Is.EqualTo(accountNumber));
            Assert.That(payee.Website, Is.EqualTo(website));
            Assert.That(payee.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(payee.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var payee = new Payee();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(payee.Name, Is.EqualTo(string.Empty));
            Assert.That(payee.AccountNumber, Is.Null);
            Assert.That(payee.Website, Is.Null);
            Assert.That(payee.PhoneNumber, Is.Null);
            Assert.That(payee.Notes, Is.Null);
            Assert.That(payee.Bills, Is.Not.Null);
            Assert.That(payee.Bills, Is.Empty);
        });
    }

    [Test]
    public void Name_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payee = new Payee();
        var name = "Water Utility";

        // Act
        payee.Name = name;

        // Assert
        Assert.That(payee.Name, Is.EqualTo(name));
    }

    [Test]
    public void AccountNumber_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payee = new Payee();
        var accountNumber = "ACC-98765";

        // Act
        payee.AccountNumber = accountNumber;

        // Assert
        Assert.That(payee.AccountNumber, Is.EqualTo(accountNumber));
    }

    [Test]
    public void Website_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payee = new Payee();
        var website = "https://waterutility.com";

        // Act
        payee.Website = website;

        // Assert
        Assert.That(payee.Website, Is.EqualTo(website));
    }

    [Test]
    public void PhoneNumber_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payee = new Payee();
        var phoneNumber = "555-9876";

        // Act
        payee.PhoneNumber = phoneNumber;

        // Assert
        Assert.That(payee.PhoneNumber, Is.EqualTo(phoneNumber));
    }

    [Test]
    public void Bills_CanAddBill_AddsCorrectly()
    {
        // Arrange
        var payee = new Payee();
        var bill = new Bill { BillId = Guid.NewGuid() };

        // Act
        payee.Bills.Add(bill);

        // Assert
        Assert.That(payee.Bills, Has.Count.EqualTo(1));
        Assert.That(payee.Bills, Contains.Item(bill));
    }

    [Test]
    public void Notes_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payee = new Payee();
        var notes = "Payment portal access required";

        // Act
        payee.Notes = notes;

        // Assert
        Assert.That(payee.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void AccountNumber_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var payee = new Payee { AccountNumber = "ACC-123" };

        // Act
        payee.AccountNumber = null;

        // Assert
        Assert.That(payee.AccountNumber, Is.Null);
    }

    [Test]
    public void Website_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var payee = new Payee { Website = "https://example.com" };

        // Act
        payee.Website = null;

        // Assert
        Assert.That(payee.Website, Is.Null);
    }

    [Test]
    public void PhoneNumber_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var payee = new Payee { PhoneNumber = "555-1234" };

        // Act
        payee.PhoneNumber = null;

        // Assert
        Assert.That(payee.PhoneNumber, Is.Null);
    }
}
