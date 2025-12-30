// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;

namespace FreelanceProjectManager.Core.Tests;

public class ProjectTests
{
    [Test]
    public void Project_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var clientId = Guid.NewGuid();
        var name = "Website Redesign";
        var description = "Complete website overhaul";
        var startDate = new DateTime(2024, 6, 1);

        // Act
        var project = new Project
        {
            ProjectId = projectId,
            UserId = userId,
            ClientId = clientId,
            Name = name,
            Description = description,
            Status = ProjectStatus.InProgress,
            StartDate = startDate,
            DueDate = startDate.AddMonths(3),
            HourlyRate = 100.00m,
            FixedBudget = 10000.00m,
            Currency = "USD",
            Notes = "High priority"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(projectId));
            Assert.That(project.UserId, Is.EqualTo(userId));
            Assert.That(project.ClientId, Is.EqualTo(clientId));
            Assert.That(project.Name, Is.EqualTo(name));
            Assert.That(project.Description, Is.EqualTo(description));
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.InProgress));
            Assert.That(project.StartDate, Is.EqualTo(startDate));
            Assert.That(project.DueDate, Is.EqualTo(startDate.AddMonths(3)));
            Assert.That(project.HourlyRate, Is.EqualTo(100.00m));
            Assert.That(project.FixedBudget, Is.EqualTo(10000.00m));
            Assert.That(project.Currency, Is.EqualTo("USD"));
            Assert.That(project.Notes, Is.EqualTo("High priority"));
            Assert.That(project.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(project.TimeEntries, Is.Not.Null);
            Assert.That(project.Invoices, Is.Not.Null);
        });
    }

    [Test]
    public void Project_DefaultValues_AreSetCorrectly()
    {
        // Act
        var project = new Project();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Name, Is.EqualTo(string.Empty));
            Assert.That(project.Description, Is.EqualTo(string.Empty));
            Assert.That(project.Currency, Is.EqualTo("USD"));
            Assert.That(project.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(project.TimeEntries, Is.Not.Null);
            Assert.That(project.TimeEntries.Count, Is.EqualTo(0));
            Assert.That(project.Invoices, Is.Not.Null);
            Assert.That(project.Invoices.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void UpdateStatus_WithNewStatus_UpdatesStatusAndTimestamp()
    {
        // Arrange
        var project = new Project { Status = ProjectStatus.Planning };
        var beforeCall = DateTime.UtcNow;

        // Act
        project.UpdateStatus(ProjectStatus.InProgress);
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.InProgress));
            Assert.That(project.UpdatedAt, Is.Not.Null);
            Assert.That(project.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void UpdateStatus_ToCompleted_SetsCompletionDate()
    {
        // Arrange
        var project = new Project { Status = ProjectStatus.InProgress };
        var beforeCall = DateTime.UtcNow;

        // Act
        project.UpdateStatus(ProjectStatus.Completed);
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.Completed));
            Assert.That(project.CompletionDate, Is.Not.Null);
            Assert.That(project.CompletionDate!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
            Assert.That(project.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateStatus_ToNonCompleted_DoesNotSetCompletionDate()
    {
        // Arrange
        var project = new Project
        {
            Status = ProjectStatus.InProgress,
            CompletionDate = null
        };

        // Act
        project.UpdateStatus(ProjectStatus.OnHold);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.OnHold));
            Assert.That(project.CompletionDate, Is.Null);
        });
    }

    [Test]
    public void GetTotalHours_WithNoTimeEntries_ReturnsZero()
    {
        // Arrange
        var project = new Project();

        // Act
        var totalHours = project.GetTotalHours();

        // Assert
        Assert.That(totalHours, Is.EqualTo(0m));
    }

    [Test]
    public void GetTotalHours_WithMultipleTimeEntries_ReturnsTotalSum()
    {
        // Arrange
        var project = new Project
        {
            TimeEntries = new List<TimeEntry>
            {
                new TimeEntry { Hours = 8.0m },
                new TimeEntry { Hours = 6.5m },
                new TimeEntry { Hours = 4.0m },
                new TimeEntry { Hours = 7.5m }
            }
        };

        // Act
        var totalHours = project.GetTotalHours();

        // Assert
        Assert.That(totalHours, Is.EqualTo(26.0m));
    }

    [Test]
    public void GetTotalHours_WithFractionalHours_CalculatesCorrectly()
    {
        // Arrange
        var project = new Project
        {
            TimeEntries = new List<TimeEntry>
            {
                new TimeEntry { Hours = 2.25m },
                new TimeEntry { Hours = 3.75m }
            }
        };

        // Act
        var totalHours = project.GetTotalHours();

        // Assert
        Assert.That(totalHours, Is.EqualTo(6.0m));
    }

    [Test]
    public void Project_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var project = new Project
        {
            DueDate = null,
            CompletionDate = null,
            HourlyRate = null,
            FixedBudget = null,
            Notes = null,
            UpdatedAt = null,
            Client = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.DueDate, Is.Null);
            Assert.That(project.CompletionDate, Is.Null);
            Assert.That(project.HourlyRate, Is.Null);
            Assert.That(project.FixedBudget, Is.Null);
            Assert.That(project.Notes, Is.Null);
            Assert.That(project.UpdatedAt, Is.Null);
            Assert.That(project.Client, Is.Null);
        });
    }

    [Test]
    public void Project_Status_CanBeSetToAllValues()
    {
        // Arrange
        var project = new Project();

        // Act & Assert
        foreach (ProjectStatus status in Enum.GetValues(typeof(ProjectStatus)))
        {
            project.Status = status;
            Assert.That(project.Status, Is.EqualTo(status));
        }
    }

    [Test]
    public void Project_HourlyRate_CanBeZero()
    {
        // Arrange & Act
        var project = new Project { HourlyRate = 0m };

        // Assert
        Assert.That(project.HourlyRate, Is.EqualTo(0m));
    }

    [Test]
    public void Project_FixedBudget_CanBeZero()
    {
        // Arrange & Act
        var project = new Project { FixedBudget = 0m };

        // Assert
        Assert.That(project.FixedBudget, Is.EqualTo(0m));
    }

    [Test]
    public void Project_Currency_CanBeSetToVariousValues()
    {
        // Arrange
        var currencies = new[] { "USD", "EUR", "GBP", "CAD", "AUD" };

        // Act & Assert
        foreach (var currency in currencies)
        {
            var project = new Project { Currency = currency };
            Assert.That(project.Currency, Is.EqualTo(currency));
        }
    }
}
