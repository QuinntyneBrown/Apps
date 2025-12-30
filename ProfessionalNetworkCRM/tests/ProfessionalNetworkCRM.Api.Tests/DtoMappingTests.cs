using ProfessionalNetworkCRM.Api.Features.Contacts;
using ProfessionalNetworkCRM.Api.Features.Interactions;
using ProfessionalNetworkCRM.Api.Features.FollowUps;

namespace ProfessionalNetworkCRM.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void ContactDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var contact = new Core.Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            ContactType = Core.ContactType.Colleague,
            Company = "Acme Corp",
            JobTitle = "Software Engineer",
            Email = "john.doe@example.com",
            Phone = "555-1234",
            LinkedInUrl = "https://linkedin.com/in/johndoe",
            Location = "San Francisco, CA",
            Notes = "Met at tech conference",
            Tags = new List<string> { "Developer", "Tech" },
            DateMet = DateTime.UtcNow.AddDays(-30),
            LastContactedDate = DateTime.UtcNow.AddDays(-5),
            IsPriority = true,
            CreatedAt = DateTime.UtcNow.AddDays(-30),
            UpdatedAt = DateTime.UtcNow.AddDays(-5),
        };

        // Act
        var dto = contact.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ContactId, Is.EqualTo(contact.ContactId));
            Assert.That(dto.UserId, Is.EqualTo(contact.UserId));
            Assert.That(dto.FirstName, Is.EqualTo(contact.FirstName));
            Assert.That(dto.LastName, Is.EqualTo(contact.LastName));
            Assert.That(dto.FullName, Is.EqualTo(contact.FullName));
            Assert.That(dto.ContactType, Is.EqualTo(contact.ContactType));
            Assert.That(dto.Company, Is.EqualTo(contact.Company));
            Assert.That(dto.JobTitle, Is.EqualTo(contact.JobTitle));
            Assert.That(dto.Email, Is.EqualTo(contact.Email));
            Assert.That(dto.Phone, Is.EqualTo(contact.Phone));
            Assert.That(dto.LinkedInUrl, Is.EqualTo(contact.LinkedInUrl));
            Assert.That(dto.Location, Is.EqualTo(contact.Location));
            Assert.That(dto.Notes, Is.EqualTo(contact.Notes));
            Assert.That(dto.Tags, Is.EqualTo(contact.Tags));
            Assert.That(dto.DateMet, Is.EqualTo(contact.DateMet));
            Assert.That(dto.LastContactedDate, Is.EqualTo(contact.LastContactedDate));
            Assert.That(dto.IsPriority, Is.EqualTo(contact.IsPriority));
            Assert.That(dto.CreatedAt, Is.EqualTo(contact.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(contact.UpdatedAt));
        });
    }

    [Test]
    public void InteractionDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var interaction = new Core.Interaction
        {
            InteractionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            InteractionType = "Email",
            InteractionDate = DateTime.UtcNow.AddDays(-2),
            Subject = "Follow-up on project",
            Notes = "Discussed project timeline",
            Outcome = "Agreed to meet next week",
            DurationMinutes = 30,
            CreatedAt = DateTime.UtcNow.AddDays(-2),
            UpdatedAt = DateTime.UtcNow.AddDays(-1),
        };

        // Act
        var dto = interaction.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.InteractionId, Is.EqualTo(interaction.InteractionId));
            Assert.That(dto.UserId, Is.EqualTo(interaction.UserId));
            Assert.That(dto.ContactId, Is.EqualTo(interaction.ContactId));
            Assert.That(dto.InteractionType, Is.EqualTo(interaction.InteractionType));
            Assert.That(dto.InteractionDate, Is.EqualTo(interaction.InteractionDate));
            Assert.That(dto.Subject, Is.EqualTo(interaction.Subject));
            Assert.That(dto.Notes, Is.EqualTo(interaction.Notes));
            Assert.That(dto.Outcome, Is.EqualTo(interaction.Outcome));
            Assert.That(dto.DurationMinutes, Is.EqualTo(interaction.DurationMinutes));
            Assert.That(dto.CreatedAt, Is.EqualTo(interaction.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(interaction.UpdatedAt));
        });
    }

    [Test]
    public void FollowUpDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var followUp = new Core.FollowUp
        {
            FollowUpId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            Description = "Schedule coffee meeting",
            DueDate = DateTime.UtcNow.AddDays(7),
            IsCompleted = false,
            Priority = "High",
            Notes = "Discuss collaboration opportunities",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = followUp.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.FollowUpId, Is.EqualTo(followUp.FollowUpId));
            Assert.That(dto.UserId, Is.EqualTo(followUp.UserId));
            Assert.That(dto.ContactId, Is.EqualTo(followUp.ContactId));
            Assert.That(dto.Description, Is.EqualTo(followUp.Description));
            Assert.That(dto.DueDate, Is.EqualTo(followUp.DueDate));
            Assert.That(dto.IsCompleted, Is.EqualTo(followUp.IsCompleted));
            Assert.That(dto.CompletedDate, Is.EqualTo(followUp.CompletedDate));
            Assert.That(dto.Priority, Is.EqualTo(followUp.Priority));
            Assert.That(dto.Notes, Is.EqualTo(followUp.Notes));
            Assert.That(dto.IsOverdue, Is.EqualTo(followUp.IsOverdue()));
            Assert.That(dto.CreatedAt, Is.EqualTo(followUp.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(followUp.UpdatedAt));
        });
    }

    [Test]
    public void FollowUpDto_ToDto_OverdueFollowUp_IsOverdueIsTrue()
    {
        // Arrange
        var followUp = new Core.FollowUp
        {
            FollowUpId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            Description = "Overdue task",
            DueDate = DateTime.UtcNow.AddDays(-7),
            IsCompleted = false,
            Priority = "High",
            CreatedAt = DateTime.UtcNow.AddDays(-10),
        };

        // Act
        var dto = followUp.ToDto();

        // Assert
        Assert.That(dto.IsOverdue, Is.True);
    }

    [Test]
    public void FollowUpDto_ToDto_CompletedFollowUp_IsOverdueIsFalse()
    {
        // Arrange
        var followUp = new Core.FollowUp
        {
            FollowUpId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            Description = "Completed task",
            DueDate = DateTime.UtcNow.AddDays(-7),
            IsCompleted = true,
            CompletedDate = DateTime.UtcNow.AddDays(-8),
            Priority = "High",
            CreatedAt = DateTime.UtcNow.AddDays(-10),
        };

        // Act
        var dto = followUp.ToDto();

        // Assert
        Assert.That(dto.IsOverdue, Is.False);
    }
}
