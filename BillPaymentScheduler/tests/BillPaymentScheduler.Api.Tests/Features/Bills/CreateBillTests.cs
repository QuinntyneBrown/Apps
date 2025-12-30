// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Api.Features.Bills;
using BillPaymentScheduler.Core;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BillPaymentScheduler.Api.Tests.Features.Bills;

[TestFixture]
public class CreateBillTests
{
    private Mock<IBillPaymentSchedulerContext> _contextMock;
    private Mock<DbSet<Bill>> _billsDbSetMock;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IBillPaymentSchedulerContext>();
        _billsDbSetMock = new Mock<DbSet<Bill>>();

        _contextMock.Setup(c => c.Bills).Returns(_billsDbSetMock.Object);
    }

    [Test]
    public async Task Handle_CreatesBill_ReturnsDto()
    {
        // Arrange
        var handler = new CreateBill.Handler(_contextMock.Object);
        var command = new CreateBill.Command
        {
            PayeeId = Guid.NewGuid(),
            Name = "Test Bill",
            Amount = 100.00m,
            DueDate = DateTime.Now.AddDays(7),
            BillingFrequency = BillingFrequency.Monthly,
            Status = BillStatus.Pending,
            IsAutoPay = false,
            Notes = "Test notes"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(command.Name, result.Name);
        Assert.AreEqual(command.Amount, result.Amount);
        Assert.AreEqual(command.PayeeId, result.PayeeId);
        Assert.AreEqual(command.BillingFrequency, result.BillingFrequency);
        Assert.AreEqual(command.Status, result.Status);
        Assert.AreEqual(command.IsAutoPay, result.IsAutoPay);
        Assert.AreEqual(command.Notes, result.Notes);

        _billsDbSetMock.Verify(m => m.Add(It.IsAny<Bill>()), Times.Once);
        _contextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
