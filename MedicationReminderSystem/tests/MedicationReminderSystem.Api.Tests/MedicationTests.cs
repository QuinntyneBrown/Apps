// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Tests;

[TestFixture]
public class MedicationTests
{
    [Test]
    public void MedicationToDto_MapsAllProperties()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Aspirin",
            MedicationType = MedicationType.Tablet,
            Dosage = "100mg",
            PrescribingDoctor = "Dr. Smith",
            PrescriptionDate = DateTime.UtcNow,
            Purpose = "Pain relief",
            Instructions = "Take with food",
            SideEffects = "Nausea",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = medication.ToDto();

        // Assert
        Assert.That(dto.MedicationId, Is.EqualTo(medication.MedicationId));
        Assert.That(dto.UserId, Is.EqualTo(medication.UserId));
        Assert.That(dto.Name, Is.EqualTo(medication.Name));
        Assert.That(dto.MedicationType, Is.EqualTo(medication.MedicationType));
        Assert.That(dto.Dosage, Is.EqualTo(medication.Dosage));
        Assert.That(dto.PrescribingDoctor, Is.EqualTo(medication.PrescribingDoctor));
        Assert.That(dto.PrescriptionDate, Is.EqualTo(medication.PrescriptionDate));
        Assert.That(dto.Purpose, Is.EqualTo(medication.Purpose));
        Assert.That(dto.Instructions, Is.EqualTo(medication.Instructions));
        Assert.That(dto.SideEffects, Is.EqualTo(medication.SideEffects));
        Assert.That(dto.IsActive, Is.EqualTo(medication.IsActive));
        Assert.That(dto.CreatedAt, Is.EqualTo(medication.CreatedAt));
    }

    [Test]
    public async Task CreateMedicationCommand_CreatesNewMedication()
    {
        // Arrange
        var medications = new List<Medication>();
        var mockSet = TestHelpers.CreateMockDbSet(medications);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<CreateMedicationCommandHandler>();

        mockContext.Setup(c => c.Medications).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateMedicationCommandHandler(mockContext.Object, mockLogger.Object);
        var command = new CreateMedicationCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Ibuprofen",
            MedicationType = MedicationType.Tablet,
            Dosage = "200mg",
            PrescribingDoctor = "Dr. Jones",
            Purpose = "Inflammation"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.MedicationType, Is.EqualTo(command.MedicationType));
        Assert.That(result.Dosage, Is.EqualTo(command.Dosage));
        Assert.That(medications.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetMedicationsQuery_ReturnsAllMedications()
    {
        // Arrange
        var medications = new List<Medication>
        {
            new Medication
            {
                MedicationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Aspirin",
                MedicationType = MedicationType.Tablet,
                Dosage = "100mg",
                CreatedAt = DateTime.UtcNow
            },
            new Medication
            {
                MedicationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Ibuprofen",
                MedicationType = MedicationType.Tablet,
                Dosage = "200mg",
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(medications);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<GetMedicationsQueryHandler>();

        mockContext.Setup(c => c.Medications).Returns(mockSet.Object);

        var handler = new GetMedicationsQueryHandler(mockContext.Object, mockLogger.Object);
        var query = new GetMedicationsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteMedicationCommand_RemovesMedication()
    {
        // Arrange
        var medicationId = Guid.NewGuid();
        var medications = new List<Medication>
        {
            new Medication
            {
                MedicationId = medicationId,
                UserId = Guid.NewGuid(),
                Name = "Aspirin",
                MedicationType = MedicationType.Tablet,
                Dosage = "100mg"
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(medications);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<DeleteMedicationCommandHandler>();

        mockContext.Setup(c => c.Medications).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new DeleteMedicationCommandHandler(mockContext.Object, mockLogger.Object);
        var command = new DeleteMedicationCommand(medicationId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(medications.Count, Is.EqualTo(0));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
