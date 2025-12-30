using WoodworkingProjectManager.Api.Features.Projects;
using WoodworkingProjectManager.Api.Features.Materials;
using WoodworkingProjectManager.Api.Features.Tools;

namespace WoodworkingProjectManager.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void ProjectDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var project = new Core.Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Description = "Test Description",
            Status = Core.ProjectStatus.InProgress,
            WoodType = Core.WoodType.Oak,
            StartDate = DateTime.UtcNow.AddDays(-10),
            CompletionDate = null,
            EstimatedCost = 500.00m,
            ActualCost = 450.00m,
            Notes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = project.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ProjectId, Is.EqualTo(project.ProjectId));
            Assert.That(dto.UserId, Is.EqualTo(project.UserId));
            Assert.That(dto.Name, Is.EqualTo(project.Name));
            Assert.That(dto.Description, Is.EqualTo(project.Description));
            Assert.That(dto.Status, Is.EqualTo(project.Status));
            Assert.That(dto.WoodType, Is.EqualTo(project.WoodType));
            Assert.That(dto.StartDate, Is.EqualTo(project.StartDate));
            Assert.That(dto.CompletionDate, Is.EqualTo(project.CompletionDate));
            Assert.That(dto.EstimatedCost, Is.EqualTo(project.EstimatedCost));
            Assert.That(dto.ActualCost, Is.EqualTo(project.ActualCost));
            Assert.That(dto.Notes, Is.EqualTo(project.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(project.CreatedAt));
        });
    }

    [Test]
    public void MaterialDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var material = new Core.Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Test Material",
            Description = "Test Description",
            Quantity = 10.5m,
            Unit = "board feet",
            Cost = 8.50m,
            Supplier = "Test Supplier",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = material.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.MaterialId, Is.EqualTo(material.MaterialId));
            Assert.That(dto.UserId, Is.EqualTo(material.UserId));
            Assert.That(dto.ProjectId, Is.EqualTo(material.ProjectId));
            Assert.That(dto.Name, Is.EqualTo(material.Name));
            Assert.That(dto.Description, Is.EqualTo(material.Description));
            Assert.That(dto.Quantity, Is.EqualTo(material.Quantity));
            Assert.That(dto.Unit, Is.EqualTo(material.Unit));
            Assert.That(dto.Cost, Is.EqualTo(material.Cost));
            Assert.That(dto.Supplier, Is.EqualTo(material.Supplier));
            Assert.That(dto.CreatedAt, Is.EqualTo(material.CreatedAt));
        });
    }

    [Test]
    public void ToolDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var tool = new Core.Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Tool",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            PurchasePrice = 299.99m,
            PurchaseDate = DateTime.UtcNow.AddMonths(-6),
            Location = "Workshop",
            Notes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = tool.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ToolId, Is.EqualTo(tool.ToolId));
            Assert.That(dto.UserId, Is.EqualTo(tool.UserId));
            Assert.That(dto.Name, Is.EqualTo(tool.Name));
            Assert.That(dto.Brand, Is.EqualTo(tool.Brand));
            Assert.That(dto.Model, Is.EqualTo(tool.Model));
            Assert.That(dto.Description, Is.EqualTo(tool.Description));
            Assert.That(dto.PurchasePrice, Is.EqualTo(tool.PurchasePrice));
            Assert.That(dto.PurchaseDate, Is.EqualTo(tool.PurchaseDate));
            Assert.That(dto.Location, Is.EqualTo(tool.Location));
            Assert.That(dto.Notes, Is.EqualTo(tool.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(tool.CreatedAt));
        });
    }
}
