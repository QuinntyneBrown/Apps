// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Api.Tests;

namespace PersonalLoanComparisonTool.Api.Tests;

[TestFixture]
public class PaymentScheduleFeatureTests
{
    [Test]
    public void PaymentScheduleDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var paymentSchedule = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            PaymentNumber = 1,
            DueDate = DateTime.Today,
            PaymentAmount = 300m,
            PrincipalAmount = 250m,
            InterestAmount = 50m,
            RemainingBalance = 9750m
        };

        // Act
        var dto = paymentSchedule.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PaymentScheduleId, Is.EqualTo(paymentSchedule.PaymentScheduleId));
            Assert.That(dto.OfferId, Is.EqualTo(paymentSchedule.OfferId));
            Assert.That(dto.PaymentNumber, Is.EqualTo(paymentSchedule.PaymentNumber));
            Assert.That(dto.DueDate, Is.EqualTo(paymentSchedule.DueDate));
            Assert.That(dto.PaymentAmount, Is.EqualTo(paymentSchedule.PaymentAmount));
            Assert.That(dto.PrincipalAmount, Is.EqualTo(paymentSchedule.PrincipalAmount));
            Assert.That(dto.InterestAmount, Is.EqualTo(paymentSchedule.InterestAmount));
            Assert.That(dto.RemainingBalance, Is.EqualTo(paymentSchedule.RemainingBalance));
        });
    }

    [Test]
    public async Task CreatePaymentScheduleCommand_CreatesPaymentSchedule()
    {
        // Arrange
        var paymentSchedules = new List<PaymentSchedule>();
        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(paymentSchedules);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.PaymentSchedules).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreatePaymentScheduleCommandHandler(mockContext.Object);
        var command = new CreatePaymentScheduleCommand(
            Guid.NewGuid(),
            1,
            DateTime.Today,
            300m,
            250m,
            50m,
            9750m
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.OfferId, Is.EqualTo(command.OfferId));
            Assert.That(result.PaymentNumber, Is.EqualTo(command.PaymentNumber));
            Assert.That(result.DueDate, Is.EqualTo(command.DueDate));
            Assert.That(result.PaymentAmount, Is.EqualTo(command.PaymentAmount));
            Assert.That(result.PrincipalAmount, Is.EqualTo(command.PrincipalAmount));
            Assert.That(result.InterestAmount, Is.EqualTo(command.InterestAmount));
            Assert.That(result.RemainingBalance, Is.EqualTo(command.RemainingBalance));
            Assert.That(paymentSchedules.Count, Is.EqualTo(1));
        });

        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetPaymentSchedulesQuery_ReturnsAllPaymentSchedules()
    {
        // Arrange
        var paymentSchedules = new List<PaymentSchedule>
        {
            new PaymentSchedule
            {
                PaymentScheduleId = Guid.NewGuid(),
                OfferId = Guid.NewGuid(),
                PaymentNumber = 1,
                DueDate = DateTime.Today,
                PaymentAmount = 300m,
                PrincipalAmount = 250m,
                InterestAmount = 50m,
                RemainingBalance = 9750m
            },
            new PaymentSchedule
            {
                PaymentScheduleId = Guid.NewGuid(),
                OfferId = Guid.NewGuid(),
                PaymentNumber = 2,
                DueDate = DateTime.Today.AddMonths(1),
                PaymentAmount = 300m,
                PrincipalAmount = 252m,
                InterestAmount = 48m,
                RemainingBalance = 9498m
            }
        };

        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(paymentSchedules);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.PaymentSchedules).Returns(mockDbSet.Object);

        var handler = new GetPaymentSchedulesQueryHandler(mockContext.Object);
        var query = new GetPaymentSchedulesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task DeletePaymentScheduleCommand_DeletesPaymentSchedule()
    {
        // Arrange
        var paymentScheduleId = Guid.NewGuid();
        var paymentSchedules = new List<PaymentSchedule>
        {
            new PaymentSchedule
            {
                PaymentScheduleId = paymentScheduleId,
                OfferId = Guid.NewGuid(),
                PaymentNumber = 1,
                DueDate = DateTime.Today,
                PaymentAmount = 300m,
                PrincipalAmount = 250m,
                InterestAmount = 50m,
                RemainingBalance = 9750m
            }
        };

        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(paymentSchedules);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.PaymentSchedules).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new DeletePaymentScheduleCommandHandler(mockContext.Object);
        var command = new DeletePaymentScheduleCommand(paymentScheduleId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(paymentSchedules.Count, Is.EqualTo(0));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
