// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class ProjectTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesProject()
    {
        var project = new Project();
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(project.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(project.Name, Is.EqualTo(string.Empty));
            Assert.That(project.Description, Is.EqualTo(string.Empty));
            Assert.That(project.Organization, Is.Null);
            Assert.That(project.Role, Is.Null);
            Assert.That(project.StartDate, Is.EqualTo(default(DateTime)));
            Assert.That(project.EndDate, Is.Null);
            Assert.That(project.Technologies, Is.Not.Null.And.Empty);
            Assert.That(project.Outcomes, Is.Not.Null.And.Empty);
            Assert.That(project.ProjectUrl, Is.Null);
            Assert.That(project.IsFeatured, Is.False);
            Assert.That(project.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(project.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void ToggleFeatured_FromFalse_SetsTrue()
    {
        var project = new Project { IsFeatured = false };
        project.ToggleFeatured();
        Assert.Multiple(() =>
        {
            Assert.That(project.IsFeatured, Is.True);
            Assert.That(project.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void ToggleFeatured_FromTrue_SetsFalse()
    {
        var project = new Project { IsFeatured = true };
        project.ToggleFeatured();
        Assert.That(project.IsFeatured, Is.False);
    }

    [Test]
    public void AddTechnology_NewTechnology_Adds()
    {
        var project = new Project();
        project.AddTechnology("C#");
        Assert.That(project.Technologies, Contains.Item("C#"));
    }

    [Test]
    public void AddTechnology_Duplicate_DoesNotAddDuplicate()
    {
        var project = new Project();
        project.AddTechnology("C#");
        project.AddTechnology("C#");
        Assert.That(project.Technologies.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddTechnology_DifferentCase_DoesNotAddDuplicate()
    {
        var project = new Project();
        project.AddTechnology("C#");
        project.AddTechnology("c#");
        Assert.That(project.Technologies.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddOutcome_NewOutcome_Adds()
    {
        var project = new Project();
        project.AddOutcome("Reduced costs by 30%");
        Assert.That(project.Outcomes, Contains.Item("Reduced costs by 30%"));
    }

    [Test]
    public void AddOutcome_Duplicate_DoesNotAddDuplicate()
    {
        var project = new Project();
        var outcome = "Improved performance";
        project.AddOutcome(outcome);
        project.AddOutcome(outcome);
        Assert.That(project.Outcomes.Count, Is.EqualTo(1));
    }

    [Test]
    public void Complete_SetsEndDate()
    {
        var project = new Project();
        var endDate = new DateTime(2024, 6, 30);
        project.Complete(endDate);
        Assert.Multiple(() =>
        {
            Assert.That(project.EndDate, Is.EqualTo(endDate));
            Assert.That(project.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void Project_WithAllProperties_SetsCorrectly()
    {
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "E-Commerce Platform",
            Description = "Built scalable platform",
            Organization = "Tech Corp",
            Role = "Lead Developer",
            StartDate = new DateTime(2023, 1, 1),
            ProjectUrl = "https://example.com",
            IsFeatured = true
        };

        Assert.Multiple(() =>
        {
            Assert.That(project.Name, Is.EqualTo("E-Commerce Platform"));
            Assert.That(project.Description, Is.EqualTo("Built scalable platform"));
            Assert.That(project.Organization, Is.EqualTo("Tech Corp"));
            Assert.That(project.Role, Is.EqualTo("Lead Developer"));
            Assert.That(project.StartDate, Is.EqualTo(new DateTime(2023, 1, 1)));
            Assert.That(project.ProjectUrl, Is.EqualTo("https://example.com"));
            Assert.That(project.IsFeatured, Is.True);
        });
    }

    [Test]
    public void AddTechnology_MultipleTechnologies_AddsAll()
    {
        var project = new Project();
        project.AddTechnology("C#");
        project.AddTechnology("Azure");
        project.AddTechnology("React");
        Assert.That(project.Technologies.Count, Is.EqualTo(3));
    }

    [Test]
    public void AddOutcome_MultipleOutcomes_AddsAll()
    {
        var project = new Project();
        project.AddOutcome("Increased revenue by 50%");
        project.AddOutcome("Reduced latency by 40%");
        Assert.That(project.Outcomes.Count, Is.EqualTo(2));
    }
}
