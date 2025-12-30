// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;

namespace FreelanceProjectManager.Core.Tests;

public class ClientTests
{
    [Test]
    public void Client_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "John Doe";
        var companyName = "Acme Corp";
        var email = "john@acmecorp.com";
        var phone = "555-1234";

        // Act
        var client = new Client
        {
            ClientId = clientId,
            UserId = userId,
            Name = name,
            CompanyName = companyName,
            Email = email,
            Phone = phone,
            Address = "123 Main St",
            Website = "https://acmecorp.com",
            Notes = "Important client",
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(client.ClientId, Is.EqualTo(clientId));
            Assert.That(client.UserId, Is.EqualTo(userId));
            Assert.That(client.Name, Is.EqualTo(name));
            Assert.That(client.CompanyName, Is.EqualTo(companyName));
            Assert.That(client.Email, Is.EqualTo(email));
            Assert.That(client.Phone, Is.EqualTo(phone));
            Assert.That(client.Address, Is.EqualTo("123 Main St"));
            Assert.That(client.Website, Is.EqualTo("https://acmecorp.com"));
            Assert.That(client.Notes, Is.EqualTo("Important client"));
            Assert.That(client.IsActive, Is.True);
            Assert.That(client.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(client.Projects, Is.Not.Null);
            Assert.That(client.Invoices, Is.Not.Null);
        });
    }

    [Test]
    public void Client_DefaultValues_AreSetCorrectly()
    {
        // Act
        var client = new Client();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(client.Name, Is.EqualTo(string.Empty));
            Assert.That(client.IsActive, Is.True);
            Assert.That(client.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(client.Projects, Is.Not.Null);
            Assert.That(client.Projects.Count, Is.EqualTo(0));
            Assert.That(client.Invoices, Is.Not.Null);
            Assert.That(client.Invoices.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void Deactivate_SetsIsActiveToFalseAndUpdatesTimestamp()
    {
        // Arrange
        var client = new Client { IsActive = true };
        var beforeCall = DateTime.UtcNow;

        // Act
        client.Deactivate();
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(client.IsActive, Is.False);
            Assert.That(client.UpdatedAt, Is.Not.Null);
            Assert.That(client.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void Deactivate_WhenAlreadyInactive_RemainsInactive()
    {
        // Arrange
        var client = new Client { IsActive = false };

        // Act
        client.Deactivate();

        // Assert
        Assert.That(client.IsActive, Is.False);
    }

    [Test]
    public void Client_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var client = new Client
        {
            CompanyName = null,
            Email = null,
            Phone = null,
            Address = null,
            Website = null,
            Notes = null,
            UpdatedAt = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(client.CompanyName, Is.Null);
            Assert.That(client.Email, Is.Null);
            Assert.That(client.Phone, Is.Null);
            Assert.That(client.Address, Is.Null);
            Assert.That(client.Website, Is.Null);
            Assert.That(client.Notes, Is.Null);
            Assert.That(client.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void Client_WithProjects_MaintainsProjectCollection()
    {
        // Arrange
        var client = new Client();
        var project1 = new Project { ProjectId = Guid.NewGuid(), Name = "Project 1" };
        var project2 = new Project { ProjectId = Guid.NewGuid(), Name = "Project 2" };

        // Act
        client.Projects.Add(project1);
        client.Projects.Add(project2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(client.Projects.Count, Is.EqualTo(2));
            Assert.That(client.Projects, Contains.Item(project1));
            Assert.That(client.Projects, Contains.Item(project2));
        });
    }

    [Test]
    public void Client_WithInvoices_MaintainsInvoiceCollection()
    {
        // Arrange
        var client = new Client();
        var invoice1 = new Invoice { InvoiceId = Guid.NewGuid(), InvoiceNumber = "INV-001" };
        var invoice2 = new Invoice { InvoiceId = Guid.NewGuid(), InvoiceNumber = "INV-002" };

        // Act
        client.Invoices.Add(invoice1);
        client.Invoices.Add(invoice2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(client.Invoices.Count, Is.EqualTo(2));
            Assert.That(client.Invoices, Contains.Item(invoice1));
            Assert.That(client.Invoices, Contains.Item(invoice2));
        });
    }

    [Test]
    public void Client_Email_CanBeValidEmailAddress()
    {
        // Arrange & Act
        var client = new Client { Email = "test@example.com" };

        // Assert
        Assert.That(client.Email, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void Client_Website_CanBeValidUrl()
    {
        // Arrange & Act
        var client = new Client { Website = "https://www.example.com" };

        // Assert
        Assert.That(client.Website, Is.EqualTo("https://www.example.com"));
    }

    [Test]
    public void Client_Phone_CanBeVariousFormats()
    {
        // Arrange
        var phoneNumbers = new[] { "555-1234", "(555) 123-4567", "+1-555-123-4567", "5551234567" };

        // Act & Assert
        foreach (var phone in phoneNumbers)
        {
            var client = new Client { Phone = phone };
            Assert.That(client.Phone, Is.EqualTo(phone));
        }
    }
}
